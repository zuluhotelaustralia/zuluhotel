CLASS({
  package: 'Scripts.Spells',
  name: 'GenZuluSpells',
  requires: [
    'Scripts.Spells.ZuluSpell',
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
      this.dao.select(COUNT())(function(c) {
        console.log("Count is ", c.count);
        if ( c.count != 0 ) return;

        console.log("Generating initial spell list.");

        var names = [
          "ControlUndead",
          "Darkness",
          "SpectresTouch",
          "AbyssalFlame",
          "AnimateDead",
          "Sacrifice",
          "WraithBreath",
          "SorcerorsBane",
          "SummonSpirit",
          "WraithForm",
          "WyvernStrike",
          "KillSpell",
          "LicheForm",
          "Plague",
          "Spellbind"
        ];

        var spells = names.map(function(n) {
          console.log("Creating spell", n);
          return this.NecroSpell.create({
            name: n
          });
        }.bind(this));


        spells.select(this.dao);
      }.bind(this));

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
