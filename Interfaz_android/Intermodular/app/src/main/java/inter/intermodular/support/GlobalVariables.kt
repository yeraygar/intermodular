package inter.intermodular.support

import inter.intermodular.models.*
import java.util.*

var currentClient = ClientModel("Error", "Error", "Error", "Error")
var currentUser: UserModel = UserModel("error", "error", "error", "error", "error", "error", false)
var currentZone : ZoneModel = ZoneModel("Error", "Error", "Error")
var currentTicket : TicketModel = TicketModel("Error", 0.0f, "Error", "Error", "Error", "Error", "Error", 1, Date(), false)
var currentTable : TableModel = TableModel("Error", "Error", true, false, "Error", 1, 0,0,5 ,"Error", "Error")
var currentFamily : FamilyModel = FamilyModel("Error", "Error", "Error")
var currentProduct : ProductModel = ProductModel("Error","name", 0, 0f,0, 0f, "Error", "Error", "Error", "Error")

//var currentLinesTicket : List<ProductModel> = listOf()
var allUsers : List<UserModel> = listOf()
var clientZones : List<ZoneModel> = listOf()
var currentZoneTables : List<TableModel> = listOf()
var allFamilies : List<FamilyModel> = listOf()
var currentProductList : List<ProductModel> = listOf()

var clientCreated = false
var loginIntents = 4;
var backLogin = true;
var backRegister = true;
var backUser = true;
var firstOpenMap : Boolean = true
var firstOpenTable :Boolean = false

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
