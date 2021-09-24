using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Server;
using ZuluContent.Zulu.Items;

namespace Scripts.Zulu.Utilities
{
    public static class ZuluUtil
    {
        private static readonly Gaussian Gaussian = new();

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

        public static double RandomGaussian(double mu, double sigma)
        {
            return Gaussian.Next(mu, sigma);
        }

        public static Type[] GetInheritedClasses(this Type parent)
        {
            return Assembly.GetAssembly(parent)?.GetTypes()
                .Where(target => target.IsClass && (target.GetInterfaces().Contains(parent) || target.IsSubclassOf(parent)) && !target.IsAbstract)
                .ToArray();
        }

        public static string TrimIndefiniteArticle(string value)
        {
            return Regex.Replace(value, "^[a|an] ", string.Empty);
        }


        public static Func<T, TR> GetFieldAccessor<T, TR>(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");
            MemberExpression member = Expression.Field(param, fieldName);
            LambdaExpression lambda = Expression.Lambda(typeof(Func<T, TR>), member, param);

            var compiled = (Func<T, TR>) lambda.Compile();
            return compiled;
        }

        public static List<Point3D> FindStaticTileByName(Mobile mobile, string name, int range = 10)
        {
            var eye = new Point3D(mobile);
            eye.Z += 14;
            return FindStaticTileByName(eye, mobile.Map, name, range);
        }

        public static List<Point3D> FindStaticTileByName(Point3D loc, Map map, string name, int range = 10)
        {
            loc.Z += 14; // Eye level adjustment
            var list = new List<Point3D>();

            for (int x = -range; x <= range; ++x)
            {
                for (int y = -range; y <= range; ++y)
                {
                    list.AddRange(map.Tiles.GetStaticTiles(loc.X + x, loc.Y + y)
                        .Where(t =>
                        {
                            var los = map.LineOfSight(loc, new Point3D(loc.X + x, loc.Y + y, t.Z + t.Height / 2 + 1));
                            var staticName = TileData.ItemTable[t.ID].Name;
                            return staticName.InsensitiveEquals(name) && los;
                        })
                        .Select(t => new Point3D(loc.X + x, loc.Y + y, t.Z)));
                }
            }

            return list;
        }

        public static Action<TSource, TTarget> BuildMapAction<TSource, TTarget>()
        {
            var list = new List<Tuple<PropertyInfo, PropertyInfo>>();
            foreach (var sourceProperty in typeof(TSource).GetProperties())
            {
                var targetProperty = typeof(TTarget).GetProperty(sourceProperty.Name);

                if (targetProperty == null)
                {
                    // Console.WriteLine(
                    //     $"Skipped mapping property for {typeof(TSource)}.{sourceProperty.Name} does not exist on {typeof(TTarget)}");
                    continue;
                }


                if (sourceProperty.PropertyType != targetProperty.PropertyType &&
                    !targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    var hasImplicitMethod = sourceProperty.PropertyType
                        .GetMethods(BindingFlags.Static | BindingFlags.Public)
                        .Any(method => method.Name == "op_Implicit" && method.ReturnType == targetProperty.PropertyType ||
                                       method.Name == "op_Explicit" && method.ReturnType == targetProperty.PropertyType);

                    if (!hasImplicitMethod)
                    {
                        // Console.WriteLine("Skipped mapping property no conversion available for " +
                        //                   $"{typeof(TSource)}.{sourceProperty.Name}(read:{sourceProperty.PropertyType}) => " +
                        //                   $"{typeof(TTarget)}.{targetProperty.Name}(write:{targetProperty.PropertyType})");
                        continue;
                    }
                }

                if (!sourceProperty.CanRead || !targetProperty.CanWrite)
                {
                    // Console.WriteLine("Skipped mapping property for " +
                    //                   $"{typeof(TSource)}.{sourceProperty.Name}(read:{sourceProperty.CanRead}) => " +
                    //                   $"{typeof(TTarget)}.{targetProperty.Name}(write:{targetProperty.CanWrite})");
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
        
        public static Func<T> ItemCreatorLambda<T>(Type type) where T : Item
        {
            var constructor = type.GetConstructor(Array.Empty<Type>());

            if (constructor == null)
            {
                throw new ArgumentOutOfRangeException(nameof(type), "No parameterless constructor found");
            }

            var expr = Expression.Lambda<Func<T>>(Expression.New(constructor), null).Compile();

            return expr;
        }
    }
}