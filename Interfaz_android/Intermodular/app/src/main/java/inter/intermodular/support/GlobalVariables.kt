package inter.intermodular.support

import inter.intermodular.models.*
import java.util.*
import kotlin.collections.HashMap

var currentClient = ClientModel("Error", "Error", "Error", "Error")
var currentUser: UserModel = UserModel("error", "error", "error", "error", "error", "error", false)
var currentZone : ZoneModel = ZoneModel("Error", "Error", "Error")
var currentTicket : TicketModel = TicketModel("Error", 0.0f, "Error", "Error", "Error", "Error", "Error", "Error", 1, Date(), false, "Error")
var currentTable : TableModel = TableModel("Error", "Error", true, false, "Error", 0, 0,0,5 ,"Error", "Error")
var currentFamily : FamilyModel = FamilyModel("Error", "Error", "Error")
//var currentProduct : ProductModel = ProductModel("Error","name", 0, 0f,0, 0f, "Error", "Error", "Error", "")

//var currentLinesTicket : List<ProductModel> = listOf()
var allUsers : List<UserModel> = listOf()
var clientZones : List<ZoneModel> = listOf()
var currentZoneTables : List<TableModel> = listOf()
var allFamilies : List<FamilyModel> = listOf()
var currentProductList : List<ProductModel> = listOf()

var ticketCreado = false;
var clientCreated = false
var loginIntents = 4;
var backLogin = true;
var backRegister = true;
var backUser = true;
var firstOpenMap : Boolean = true
var firstOpenTable :Boolean = false
var toReset : Boolean = true

var familyAndProducts : HashMap<String, List<ProductModel>> = HashMap()

var bool = true


var defaultAdmin : UserPost = UserPost(
    name = "Admin",
    passw = "1234",
    id_client = currentClient._id,
    rol = "Admin"
)

var defaultTable : TablePost = TablePost(
    name = "11",
    id_user = currentUser._id,
    num_column = 0,
    num_row = 0,
    id_zone = currentZone._id,
    comensalesMax = 6
)

var defaultZone : ZonePost = ZonePost(
    zone_name = "Comedor",
    id_client = currentClient._id
)

var defaultFamily : FamilyPost = FamilyPost(
    name = "Barril",
    id_client = currentClient._id
)

var defaultProduct1 : ProductPost = ProductPost(
    name = "Caña",
    precio = 1.5f,
    cantidad = 1,
    total = 1.5f,
    id_client = currentClient._id,
    id_familia = "Error",
    id_ticket = "Error"
)
var defaultProduct2 : ProductPost = ProductPost(
    name = "Pinta",
    precio = 1.5f,
    cantidad = 1,
    total = 1.5f,
    id_client = currentClient._id,
    id_familia = "Error",
    id_ticket = "Error"
)


