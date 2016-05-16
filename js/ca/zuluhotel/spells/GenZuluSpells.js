CLASS({
  package: 'ca.zuluhotel.spells',
  name: 'GenZuluSpells',
  requires: [
    'ca.zuluhotel.spells.ZuluSpell',
    'foam.dao.EasyDAO',
    'foam.node.dao.JSONFileDAO'
  ],
  properties: [
    {
      name: 'outpath',
      type: 'String',
      help: 'Target path to put generated spells in',
    },
    {
      name: 'fs',
      hidden: true,
      type: 'foam.node.NodeRequire'
    },
    {
      name: 'path',
      hidden: true,
      type: 'foam.node.NodeRequire'
    },
    {
      name: 'dao',
      hidden: true,
      factory: function() {
        return this.EasyDAO.create({
          model: this.ZuluSpell,
          daoType: this.JSONFileDAO.xbind({
            model: this.ZuluSpell,
            filename: 'js/ca/zuluhotel/spells/Spells.json'
          })
        });
      }
    }
  ],
  methods: [
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
    function execute() {
      this.dao.select({
        put: function(spell) {
//          var dest = this.outpath + this.path.sep + spell.type + this.path.sep + spell.className + '.cs';

          //          this.fs.writeFileSync(dest, spell.toCS());

          var base = this.outpath + this.path.sep + spell.type + this.path.sep;
          var dest = base + spell.scrollName + '.cs';
          this.ensurePath(base);
          console.log("Writing ", dest);
          this.fs.writeFileSync(dest, spell.toScroll());
        }.bind(this),
        eof: function() {
          process.exit(0);
        }
      });
    }
  ]
});
