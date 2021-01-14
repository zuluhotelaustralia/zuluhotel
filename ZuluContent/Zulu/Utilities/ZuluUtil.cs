using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using Server;

namespace Scripts.Zulu.Utilities
{
    public static class ZuluUtil
    {
        private static Gaussian m_Gaussian = new Gaussian();

        public static IEnumerable<InterfaceMapping> GetAllInterfaceMaps(this Type t) =>
            t.GetTypeInfo()
                .ImplementedInterfaces
                .Select(t.GetInterfaceMap);

        public static ILookup<MethodInfo, Type> GetMethodsForInterfaces(this Type aType) =>
            aType.GetAllInterfaceMaps()
                .SelectMany(im => im.TargetMethods.Select(tm => new {im.TargetType, im.InterfaceType, tm}))
                .ToLookup(imtm => imtm.tm, imtm => imtm.InterfaceType);


        public static Type[] GetInterfacesForMethod(this MethodInfo mi) =>
            mi.ReflectedType
                .GetAllInterfaceMaps()
                .Where(im => im.TargetMethods.Any(tm => tm == mi))
                .Select(im => im.InterfaceType)
                .ToArray();

        public static bool IsOverride(this MethodInfo m)
        {
            return m.GetBaseDefinition().DeclaringType != m.DeclaringType;
        }

        // private static readonly Func<BinaryFileReader, BinaryReader> GetBinaryReader =
        //     GetFieldAccessor<BinaryFileReader, BinaryReader>("m_File");
        //
        // public static Stream GetBinaryFileReaderBaseStream(IGenericReader reader)
        // {
        //     if (reader is BinaryFileReader binaryFileReader)
        //     {
        //         return ZuluUtil.GetBinaryReader(binaryFileReader).BaseStream;
        //     }
        //
        //     return null;
        // }

        // public static void Write<T>(this IGenericWriter stream, T value) where T : struct
        // {
        //     var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
        //     var span = MemoryMarshal.AsBytes(tSpan);
        //     stream.Write(span.ToArray());
        // }
        //
        // public static T Read<T>(this IGenericReader reader) where T : struct
        // {
        //     var result = default(T);
        //     var tSpan = MemoryMarshal.CreateSpan(ref result, 1);
        //     var span = MemoryMarshal.AsBytes(tSpan);
        //     GetBinaryFileReaderBaseStream(reader).Read(span);
        //     return result;
        // }

        public static double RandomGaussian(double mu, double sigma)
        {
            return m_Gaussian.Next(mu, sigma);
        }

        public static Type[] GetInheritedClasses(this Type parent)
        {
            return Assembly.GetAssembly(parent)?.GetTypes()
                .Where(target => target.GetInterfaces().Contains(parent) && !target.IsAbstract)
                .ToArray();
        }

        public static Stream GetBaseStream(this IGenericReader reader)
        {
            var field = reader.GetType().GetField("m_File", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                var baseReader = (BinaryReader) field.GetValue(reader);
                if (baseReader != null)
                    return baseReader.BaseStream;
            }

            return null;
        }

        // public static Stream GetBaseStream(this IGenericWriter writer)
        // {
        //     switch (writer)
        //     {
        //         case BinaryFileWriter binaryFileWriter:
        //             return binaryFileWriter.UnderlyingStream;
        //             break;
        //         case BufferedFileWriter bufferedFileWriter:
        //             break;
        //         case AsyncWriter asyncWriter:
        //             return asyncWriter.MemStream;
        //             break;
        //         default:
        //             throw new ArgumentException($"Stream type not supported {nameof(writer)}");
        //     }
        //
        //     return null;
        // }

        public static Func<T, TR> GetFieldAccessor<T, TR>(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");
            MemberExpression member = Expression.Field(param, fieldName);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<T, TR>), member, param);

            var compiled = (Func<T, TR>) lambda.Compile();
            return compiled;
        }


        public static Action<TSource, TTarget> BuildMapAction<TSource, TTarget>()
        {
            var list = new List<Tuple<PropertyInfo, PropertyInfo>>();
            foreach (var sourceProperty in typeof(TSource).GetProperties())
            {
                var targetProperty = typeof(TTarget).GetProperty(sourceProperty.Name);

                if (targetProperty == null)
                {
                    Console.WriteLine(
                        $"Skipped mapping property for {typeof(TSource)}.{sourceProperty.Name} does not exist on {typeof(TTarget)}");
                    continue;
                }


                if (sourceProperty.PropertyType != targetProperty.PropertyType &&
                    !targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    bool hasImplicitMethod = (
                        from method in sourceProperty.PropertyType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                        where method.Name == "op_Implicit" && method.ReturnType == targetProperty.PropertyType ||
                              method.Name == "op_Explicit" && method.ReturnType == targetProperty.PropertyType
                        select method
                    ).Any();

                    if (!hasImplicitMethod)
                    {
                        Console.WriteLine("Skipped mapping property no conversion available for " +
                                          $"{typeof(TSource)}.{sourceProperty.Name}(read:{sourceProperty.PropertyType}) => " +
                                          $"{typeof(TTarget)}.{targetProperty.Name}(write:{targetProperty.PropertyType})");
                        continue;
                    }
                }

                if (!sourceProperty.CanRead || !targetProperty.CanWrite)
                {
                    Console.WriteLine("Skipped mapping property for " +
                                      $"{typeof(TSource)}.{sourceProperty.Name}(read:{sourceProperty.CanRead}) => " +
                                      $"{typeof(TTarget)}.{targetProperty.Name}(write:{targetProperty.CanWrite})");
                    continue;
                }

                list.Add(Tuple.Create(sourceProperty, targetProperty));
            }

            return BuildMapAction<TSource, TTarget>(list);
        }

        public static Action<TSource, TTarget> BuildMapAction<TSource, TTarget>(
            IEnumerable<Tuple<PropertyInfo, PropertyInfo>> properties)
        {
            var source = Expression.Parameter(typeof(TSource), "source");
            var target = Expression.Parameter(typeof(TTarget), "target");

            var statements = new List<Expression>();
            foreach (var (sourcePropertyInfo, targetPropertyInfo) in properties)
            {
                var sourceProperty = Expression.Property(source, sourcePropertyInfo);
                var targetProperty = Expression.Property(target, targetPropertyInfo);

                Expression value = sourceProperty;

                if (value.Type != targetProperty.Type)
                    value = Expression.Convert(value, targetProperty.Type);

                Expression statement = Expression.Assign(targetProperty, value);
                // for class/interface or nullable type
                if (!sourceProperty.Type.IsValueType || Nullable.GetUnderlyingType(sourceProperty.Type) != null)
                {
                    var valueNotNull =
                        Expression.NotEqual(sourceProperty, Expression.Constant(null, sourceProperty.Type));
                    statement = Expression.IfThen(valueNotNull, statement);
                }

                statements.Add(statement);
            }

            var body = statements.Count == 1 ? statements[0] : Expression.Block(statements);
            // for class.interface type
            if (!source.Type.IsValueType)
            {
                var sourceNotNull = Expression.NotEqual(source, Expression.Constant(null, source.Type));
                body = Expression.IfThen(sourceNotNull, body);
            }

            // not sure about the need of this
            if (body.CanReduce)
                body = body.ReduceAndCheck();
            body = body.ReduceExtensions();

            var lambda = Expression.Lambda<Action<TSource, TTarget>>(body, source, target);

            return lambda.Compile();
        }
    }
}