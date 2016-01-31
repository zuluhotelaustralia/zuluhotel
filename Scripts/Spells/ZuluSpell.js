CLASS({
  package: 'Scripts.Spells',
  name: 'ZuluSpell',
  ids: ['name'],
  properties: [
    {
      name: 'name',
      type: 'String'
    },
    {
      name: 'label',
      defaultValueFn: function() {
        var name = this.name;
        return name.replace(/[a-z][^0-9a-z_]/g, function(a) {
          return a.substring(0,1) + ' ' + a.substring(1,2);
        });
      },
      type: 'String'
    },
    {
      name: 'type',
      type: 'String'
    },
    {
      name: 'wordsOfPower',
      type: 'String'
    },
    {
      name: 'targetted',
      type: 'Boolean',
      defaultValue: true
    },
    {
      name: 'baseClass',
      defaultValueFn: function() {
        return this.type === 'Necromancy' ? 'NecromancerSpell' : 'Spell';
      }
    },
    {
      name: 'className',
      defaultValueFn: function() { return this.name + 'Spell'; }
    },
    {
      name: 'harmful',
      type: 'Boolean',
      defaultValue: true
    }
  ],
  templates: [
    { name: 'toCS' }
  ]
});
