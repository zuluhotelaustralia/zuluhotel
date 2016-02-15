CLASS({
  name: 'OreDAOTrait',
  package: 'ca.zuluhotel',
  properties: [
    {
      name: 'oreDAO',
      factory: function() {
        return this.EasyDAO.create({
          model: this.Ore,
          daoType: this.JSONFileDAO.xbind({
            model: this.Ore,
            filename: 'js/data/ores.json'
          })
        });
      }
    }
  ]
});
