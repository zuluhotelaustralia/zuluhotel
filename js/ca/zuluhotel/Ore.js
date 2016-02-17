CLASS({
  package: 'ca.zuluhotel',
  name: 'Ore',
  properties: [
    {
      name: 'name'
    },
    {
      name: 'className',
      defaultValueFn: function() { return this.name; }
    },
    {
      name: 'hue',
      type: 'Int'
    },
    {
      name: 'labelNumber',
      type: 'Int',
      defaultValue: 1042853
    },
    {
      name: 'ingotLabelNumber',
      type: 'Int',
      defaultValue: 1042692;
    },
    {
      name: 'smeltDifficulty',
      type: 'Float',
      defaultValue: 50
    },
    {
      name: 'minSkill',
      type: 'Float'
    },
    {
      name: 'maxSkill',
      help: 'Skill at which mining this ore will not fail'
    },
    {
      name: 'reqSkill',
      help: 'Minimum skill before its even possible to harvest this resource'
    },
    {
      name: 'veinChance',
      type: 'Float'
    },
    {
      name: 'abundance',
      defaultValue: 1,
      type: 'Float'
    },
    {
      name: 'fallbackChance',
      type: 'Float',
      defaultValue: 0.5
    },
    {
      name: 'craftAttributeName',
      defaultValueFn: function() {
        return this.name == 'Iron' ? 'Blank' : this.name;
      }
    }
  ],
  templates: [
    { name: 'asItem' },
    { name: 'asIngot' }
  ]
});
