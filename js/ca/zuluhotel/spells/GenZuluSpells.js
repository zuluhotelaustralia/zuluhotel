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
        console.log("cwd", process.cwd());
        return this.EasyDAO.create({
          model: this.ZuluSpell,
          daoType: this.JSONFileDAO.xbind({
            model: this.ZuluSpell,
            filename: 'Scripts/Spells/Spells.json'
          })
        });
      }
    }
  ],
  methods: [
    function execute() {
      this.dao.select({
        put: function(spell) {

          var dest = this.outpath + this.path.sep + spell.type + this.path.sep + spell.className + '.cs';

          console.log("Writing ", dest);
          this.fs.writeFileSync(dest, spell.toCS());
        }.bind(this),
        eof: function() {
          process.exit(0);
        }
      });
    }
  ]
});
