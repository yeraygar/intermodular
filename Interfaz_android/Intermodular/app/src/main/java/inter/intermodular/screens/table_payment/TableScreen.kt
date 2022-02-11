package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.combinedClickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.HighlightOff
import androidx.compose.material.icons.filled.Menu
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavController
import androidx.navigation.NavGraph.Companion.findStartDestination
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.models.ProductModel
import inter.intermodular.support.*
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

@Composable
fun TableScreen(navController: NavController, tableViewModel : TableViewModel){

    val scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()
    val snackbarHostState = remember { SnackbarHostState()}
    val title = remember { mutableStateOf("") }
    val isDialogOpen = remember { mutableStateOf(false)}
    val afterFirstProduct = remember { mutableStateOf(false)}
    val totalBill = remember { mutableStateOf(0.0f)}
    val productClicked = remember { mutableStateOf(false)}

    var familyProductList : MutableState<List<ProductModel>> = remember { mutableStateOf(listOf())}
   // var currentTicketLines : MutableState<MutableList<ProductModel>> = remember { mutableStateOf(mutableListOf())}
    val currentTicketLines = remember { mutableStateOf(listOf<ProductModel>()) }

/*    if(currentTicketLines.value.isEmpty()){
        tableViewModel.hasOpenTicket(currentTable._id)
        if (!tableViewModel.openTicketResponse.isNullOrEmpty())
            if(currentTable._id == tableViewModel.openTicketResponse[0].id_table){
                currentTable.ocupada = true
                currentTicket = tableViewModel.openTicketResponse[0]
                tableViewModel.getTicketLines(currentTicket._id)
                if(!tableViewModel.ticketLinesResponse.isNullOrEmpty())
                    //currentTicketLines.value.addAll(0,tableViewModel.ticketLinesResponse)
                        for(line in tableViewModel.ticketLinesResponse){
                            currentTicketLines.value = currentTicketLines.value + line
                        }
            }
    }*/

    if(currentTicketLines.value.isNotEmpty()){
        afterFirstProduct.value = false
        for(line in currentTicketLines.value)
            Logger.d(" $line \n $currentTable \n $currentTicket ")
    }


    if(firstOpenTable){
        currentTicketLines.value = listOf()
        tableViewModel.resetTableViewModel()
    }


    if(currentTicketLines.value.isEmpty() || firstOpenTable){
        tableViewModel.recuperaMesa(currentTable._id)
        Logger.wtf(currentTicketLines.value.toString())
        Logger.wtf(currentTable._id + currentTable.name)
        Logger.wtf("Ticket" + currentTable.id_ticket)
        currentTicketLines.value = tableViewModel.ticketLinesResponse
        if(currentTicketLines.value.isNotEmpty()) productClicked.value = true
        firstOpenTable = false

    }

    title.value = "${currentTable.name} - ${currentUser.name}"

    if(productClicked.value){
        recalculate(currentTicketLines, totalBill)
        productClicked.value = false
    }



    tableViewModel.getClientFamilies(currentClient._id)
    //(!tableViewModel.clientFamiliesResponse.isNullOrEmpty() && firstOpenTable){
    //firstOpenTable = false
    TableStart(
        navController = navController,
        tableViewModel = tableViewModel,
        scaffoldState = scaffoldState,
        scope = scope,
        snackbarHostState = snackbarHostState,
        title = title,
        isDialogOpen = isDialogOpen,
        familyProductList = familyProductList,
        currentTicketLines = currentTicketLines,
        totalBill = totalBill

    )
    if (isDialogOpen.value)
        ShowAlertDialogFamilyProducts(
            isDialogOpen = isDialogOpen,
            tableViewModel = tableViewModel,
            scope = scope,
            familyProductList = familyProductList,
            currentTicketLines = currentTicketLines,
            afterFirstProduct = afterFirstProduct,
            totalBill = totalBill,
            productClicked = productClicked
        )
    // }
}

fun recalculate(
    currentTicketLines: MutableState<List<ProductModel>>,
    totalBill: MutableState<Float>
) {
    totalBill.value = 0.0f
    if (!currentTicketLines.value.isNullOrEmpty())
        for (line in currentTicketLines.value) {
            line.total = line.cantidad * line.precio
            totalBill.value = totalBill.value + line.total
        }
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun TableStart(
    navController: NavController,
    tableViewModel: TableViewModel,
    snackbarHostState: SnackbarHostState,
    scope: CoroutineScope,
    scaffoldState: ScaffoldState,
    title: MutableState<String>,
    isDialogOpen: MutableState<Boolean>,
    familyProductList: MutableState<List<ProductModel>>,
    currentTicketLines: MutableState<List<ProductModel>>,
    totalBill: MutableState<Float>,
) {

    allFamilies = tableViewModel.clientFamiliesResponse

    Scaffold(

        scaffoldState = scaffoldState,
        snackbarHost = {
            SnackbarHost(
                hostState = snackbarHostState,
                snackbar = {
                    Text(text = "aqui el grueso")

                    //TODO component snackBar, en este Scaffold no haria falta
                }
            )
        },

        drawerShape = MaterialTheme.shapes.small,
        drawerBackgroundColor = Color.White,
        drawerContent = {
            //TODO component drawer botones de accion
            Text(text = "aqui el drawer")

        },

        topBar = {
            TopAppBar(
                backgroundColor = colorResource(id = R.color.azul_oscuro),
                title = {
                    Text(
                        text = title.value,
                        modifier = Modifier
                            .padding(30.dp,0.dp,0.dp,0.dp),
                        color = colorResource(id = R.color.white)
                    ) },
                navigationIcon = {
                    IconButton(
                        onClick = {
                            Logger.d("Click en options icon")
                            scope.launch { scaffoldState.drawerState.open() }
                        }) {
                        Icon(Icons.Filled.Menu, contentDescription = null, tint = Color.White)
                    }
                },
                actions = {
                    IconButton(onClick = {
                        if(!currentTicketLines.value.isNullOrEmpty()){
                            currentTable.id_ticket = currentTicket._id
                            Logger.wtf("Mesa cerrada llena \nCurrentTicket ${currentTicket._id} && Table ${currentTable.id_ticket}")
                            tableViewModel.updateTable(currentTable, currentTable._id)
                            currentTicketLines.value = listOf()
                        }else{
                            if(currentTable.id_ticket != "Error")
                                tableViewModel.deleteTicket(currentTable.id_ticket)
                            currentTable.id_ticket = "Error"
                            currentTable.ocupada = false
                            tableViewModel.updateTable(currentTable, currentTable._id)
                            Logger.d("Cerrar mesa vacia")
                        }
                        navController.navigate(ScreenNav.MapScreen.route){
                            popUpTo(navController.graph.findStartDestination().id){saveState = false}
                            restoreState = true
                        }
                        firstOpenTable = true

                    }) {
                        Icon(Icons.Filled.HighlightOff, contentDescription = "Cerrar", tint = Color.White)
                    }
                }
            )
        }
    ) {
        Column(
            modifier = Modifier.fillMaxSize()
        ) {
            Card(
                backgroundColor = colorResource(id = R.color.gris_muy_claro),
                modifier = Modifier
                    .fillMaxWidth()
                    .fillMaxHeight(0.25f)
                    .padding(5.dp)
            ) {
                LazyVerticalGrid(
                    cells = GridCells.Adaptive(100.dp),
                    modifier = Modifier.fillMaxSize()
                ){
                    for( i in 0 until tableViewModel.clientFamiliesResponse.count()){
                        item {
                            Button(
                                modifier = Modifier
                                    .height(80.dp)
                                    .width(100.dp)
                                    .padding(5.dp),

                                colors = ButtonDefaults.buttonColors(colorResource(id = R.color.azul_oscuro)),
                                onClick = {
                                    currentFamily = tableViewModel.clientFamiliesResponse[i]
                                    Logger.i("Familia seleccionada $currentFamily")

                                    scope.launch{
                                        tableViewModel.getFamilyProducts(tableViewModel.clientFamiliesResponse[i]._id)
                                        delay(100)
                                        familyProductList.value = tableViewModel.familyProductsResponse

                                        currentProductList = tableViewModel.familyProductsResponse
                                        isDialogOpen.value = true

                                    }

                                }) {
                                Text(
                                    text =  if (allFamilies[i].name.length > 5)
                                        allFamilies[i].name.substring(0, 5)
                                    else allFamilies[i].name,
                                    fontSize = 16.sp,
                                    color = Color.White,
                                    fontWeight = FontWeight.Bold
                                )
                            }
                        }
                    }
                }
            }

            TicketHeadComponent(totalBill = totalBill)

            if(!currentTicketLines.value.isNullOrEmpty()) {
                TicketContentComponent(currentTicketLines)
            }
        }
    }
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
private fun TicketContentComponent(currentTicketLines: MutableState<List<ProductModel>>) {
    Card(
        backgroundColor = colorResource(id = R.color.gris_muy_claro),
        modifier = Modifier
            .fillMaxWidth()
            .fillMaxHeight(1f)
            .padding(5.dp)
    ) {
        LazyVerticalGrid(
            cells = GridCells.Fixed(5),
            horizontalArrangement = Arrangement.Center,
            verticalArrangement = Arrangement.Top,
            contentPadding = PaddingValues(5.dp),
        ) {
            for (i in 0 until currentTicketLines.value.count()) {
                currentTicketLines.value[i].total = currentTicketLines.value[i].cantidad * currentTicketLines.value[i].precio

                item {
                    Card(
                        modifier = Modifier.combinedClickable(onLongClick = {
                            Logger.wtf("LONG CLICK!!")
                        }){}
                    ){
                        Text(
                            text = "${currentTicketLines.value[i].cantidad}",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(10.dp)
                        )
                    }
                }
                item(span = { GridItemSpan(2) }) {
                    Card(
                        modifier = Modifier.combinedClickable(onLongClick = {
                            Logger.wtf("LONG CLICK!!")
                        }){}
                    ){
                        Text(
                            text = currentTicketLines.value[i].name,
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(10.dp)
                        )
                    }
                }
                item {
                    Card(
                        modifier = Modifier.combinedClickable(onLongClick = {
                            Logger.wtf("LONG CLICK!!")
                        }){}
                    ){
                        Text(
                            text = "${currentTicketLines.value[i].precio}€",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(10.dp)
                        )
                    }
                }
                item {
                    Card(
                        modifier = Modifier.combinedClickable(onLongClick = {
                            Logger.wtf("LONG CLICK!!")
                        }){}
                    ){
                        Text(
                            text = "${currentTicketLines.value[i].total}€",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(10.dp)
                        )
                    }
                }
            }
        }
    }
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
private fun TicketHeadComponent(totalBill: MutableState<Float>) {
    Card(
        backgroundColor = colorResource(id = R.color.gris_muy_claro),
        modifier = Modifier
            .fillMaxWidth()
            .padding(5.dp)
    ) {
        LazyVerticalGrid(
            cells = GridCells.Fixed(5),
            contentPadding = PaddingValues(5.dp),
            //modifier = Modifier.padding(5.dp)
        ) {
            item(span = { GridItemSpan(5) }) {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                    ) {
                        Text(
                            text = "TOTAL CUENTA  :   ${totalBill.value}€ ",
                            modifier = Modifier
                                .padding(30.dp, 10.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 16.sp,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item {

                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "CANT.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item(span = { GridItemSpan(2) }) {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        // .padding(10.dp)
                    ) {
                        Text(
                            text = "NOMBRE",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }

            }
            item {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "$/Un.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontSize = 12.sp,
                            fontWeight = FontWeight.ExtraBold,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "TOT.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
        }
    }
}


