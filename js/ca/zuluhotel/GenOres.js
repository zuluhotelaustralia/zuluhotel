CLASS({
  package: 'ca.zuluhotel',
  name: 'GenOres',
  traits: [
    'ca.zuluhotel.OreDAOTrait'
  ],
  properties: [
    {
      name: 'itemsPath',
      preSet: function(_, s) { return this.path.normalize(s); }
    },
    {
      name: 'baseOre',
      preSet: function(_, s) { return this.path.normalize(s); }
    },
    {
      name: 'miningResources',
      preSet: function(_, s) { return this.path.normalize(s); }
    },
    {
      name: 'resourceInfo',
      preSet: function(_, s) { return this.path.normalize(s); }
    },
    {
      name: 'fs',
      type: 'foam.node.NodeRequire'
    },
    {
      name: 'path',
      type: 'foam.node.NodeRequire'
    }
  ],
  requires: [
    'ca.zuluhotel.Ore',
    'foam.dao.EasyDAO',
    'foam.node.dao.JSONFileDAO'
  ],
  methods: [
    function aseq(/* ... afuncs */) {
      if ( arguments.lenth == 0 ) return anop;

      var f = arguments[arguments.length-1].bind(this);

      for ( var i = arguments.length-2 ; i >= 0 ; i-- )
        f = arguments[i].aseq(i % 100 == 99 ? atramp(f.bind(this)) : f.bind(this));

      return f.bind(this);
    },
    function execute() {
      aseq(
        this.anormalize(),
        this.agenItems(),
        this.abaseOre(),
        this.aminingResources(),
        this.aresourceInfo()
      )(function() {
      });
    },
    function ensurePath(p) {
      var path = p.split(this.path.sep);
      var s = path[0];
      for ( var i = 1 ; i <= path.length ; s += this.path.sep + path[i++] ) {
        console.log("Checking", s);
        try {
          var stat = this.fs.statSync(s);
          if ( ! stat.isDirectory ) {
            throw new Error(s, " is a file, cannot create subdirectory under a file");
          }
        } catch(e) {
          if ( e.code && e.code == 'ENOENT' ) {
            this.fs.mkdirSync(s);
          } else {
            throw e;
          }
        }
      }
    },
    function agenItems() {
      return aif(
        !this.itemsPath,
        function(ret) {
          console.log("Skipping itmes, itemsPath parameter not set");
          ret();
        },
        this.aseq(
          function(ret) {
            console.log("Checking output directory.");
            this.ensurePath(this.itemsPath);
            ret();
          },
          function(ret) {
            this.oreDAO.select({
              put: function(o) {
                var output = this.itemsPath + this.path.sep + o.className + 'Ore.cs';
                console.log("Writing", output);
                this.fs.writeFileSync(output, o.asItem());

                output = this.itemsPath + this.path.sep + o.className + 'Ingot.cs';
                console.log("Writing", output);
                this.fs.writeFileSync(output, o.asIngot());
              }.bind(this),
              eof: function() {
                ret();
              }
            });
          }));
    },
      function abaseOre(ret) {
        return aif(
          !this.baseOre,
          function(ret) {
            console.log("Skipping BaseOre.cs, baseOre parameter not set.");
            ret();
          },
          this.aseq(
            function(ret) {
              this.oreDAO.orderBy(this.Ore.MIN_SKILL).select()(ret);
            },
            function(ret, ores) {
              var output = this.baseOre;
              this.ensurePath(output.substring(0, output.lastIndexOf(this.path.sep)));
              console.log("Writing ", output);
              this.fs.writeFileSync(output, this.BaseOre_CS(undefined, ores));
              ret();
            }));
      },
    function aminingResources(ret) {
      return aif(
        !this.miningResources,
        function(ret) {
          console.log("Skipping MiningResources.cs, miningResources parameter not set.");
          ret();
        },
        this.aseq(
          function(ret) {
            this.oreDAO.orderBy(this.Ore.MIN_SKILL).select()(ret);
          },
          function(ret, ores) {
            var output = this.miningResources;
            this.ensurePath(output.substring(0, output.lastIndexOf(this.path.sep)));

            console.log("Writing ", output);
            this.fs.writeFileSync(output, this.MiningResources_CS(undefined, ores));
            ret();
          }));
    },
    function aresourceInfo(ret) {
      return aif(
        !this.resourceInfo,
        function(ret) {
          console.log("Skipping ResourceInfo.cs, resourceInfo parameter not set.");
          ret();
        },
        this.aseq(
          function(ret) {
            this.oreDAO.orderBy(this.Ore.MIN_SKILL).select()(ret);
          },
          function(ret, ores) {
            var output = this.resourceInfo;
            this.ensurePath(output.substring(0, output.lastIndexOf(this.path.sep)));

            console.log("Writing ", output);
            this.fs.writeFileSync(output, this.ResourceInfo_CS(undefined, ores));
            ret();
          }));
    },
    function anormalize(ret) {
      return this.aseq(
        function(ret) {
          console.log("Normalizing abundance values...");
          this.oreDAO.select(SUM(this.Ore.ABUNDANCE))(ret);
        },
        function(ret, s) {
          var sum = s.value;
          this.oreDAO.update({
            f: function(ore) {
              ore.veinChance = ore.abundance / sum * 100;
            }
          })(ret);
        });
    }
  ],
  templates: [
    { name: 'MiningResources_CS' },
    { name: 'ResourceInfo_CS' },
    { name: 'BaseOre_CS' }
  ],
});
