package inter.intermodular.support

import inter.intermodular.models.*

var currentClient = ClientModel("Error", "Error", "Error", "Error")
var currentUser: UserModel = UserModel("error", "error", "error", "error", "error", "error", false)
var currentZone : ZoneModel = ZoneModel("Error", "Error", "Error")
var allUsers : List<UserModel> = listOf()
var clientCreated = false
var loginIntents = 4;
var backLogin = true;
var backRegister = true;
var backUser = true;
var clientZones : List<ZoneModel> = listOf()
var currentZoneTables : List<TableModel> = listOf()
var currentTable : TableModel? = null
var firstOpenMap : Boolean = true


val defaultAdmin : UserPost = UserPost(
    name = "Admin",
    passw = "1234",
    id_client = currentClient._id,
    rol = "Admin"
)

val defaultTable : TablePost = TablePost(
    name = "11",
    id_user = currentUser._id,
    num_column = 0,
    num_row = 0,
    id_zone = currentZone._id,
    comensalesMax = 6
)

val defaultZone : ZonePost = ZonePost(
    zone_name = "Comedor",
    id_client = currentClient._id
)
