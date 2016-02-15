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
    function execute() {
      console.log("Generation currently disabled.");
      this.dao.select({
        put: function(spell) {
	  console.log("Found spell", spell.name, spell.wordsOfPower);
	  return;

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
