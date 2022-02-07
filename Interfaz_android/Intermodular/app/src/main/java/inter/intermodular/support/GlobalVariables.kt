package inter.intermodular.support

import inter.intermodular.models.ClientModel
import inter.intermodular.models.TableModel
import inter.intermodular.models.ZoneModel

var currentClient = ClientModel("Error", "Error", "Error", "Error")
var clientCreated = false
var loginIntents = 3;
var backLogin = true;
var backRegister = true;
var clientZones : List<ZoneModel> = listOf()
var currentZone : ZoneModel? = null
var currentZoneTables : List<TableModel> = listOf()
var currentTable : TableModel? = null
var firstOpenMap : Boolean = true