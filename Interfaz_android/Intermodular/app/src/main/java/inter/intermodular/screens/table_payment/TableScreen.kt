package inter.intermodular.screens.table_payment

import android.content.Context
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Rect
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.Outline
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.Density
import androidx.compose.ui.unit.LayoutDirection
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import androidx.navigation.NavGraph.Companion.findStartDestination
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.models.ProductModel
import inter.intermodular.support.*
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch

@Composable
fun TableScreen(
    navController: NavController,
    tableViewModel: TableViewModel,
    applicationContext: Context
){
    val scope = rememberCoroutineScope()
    val snackbarHostState = remember { SnackbarHostState()}
    val title = remember { mutableStateOf("") }
    val afterFirstProduct = remember { mutableStateOf(false)}
    val totalBill = remember { mutableStateOf(0.0f)}
    val productClicked = remember { mutableStateOf(false)}

    var currentLine = remember { mutableStateOf(
        ProductModel("Error", "Error",0, 0.0f, 0, 0.0f,
            "Error", "Error", "Error", "")
    )}

    val scaffoldState = rememberScaffoldState()
    val isDialogOpen = remember { mutableStateOf(false)}
    val isComensalesOpen = remember { mutableStateOf(false)}
    val isCobrarOpen = remember { mutableStateOf(false)}
    val isLineOptionsOpen = remember { mutableStateOf(false)}

    var familyProductList : MutableState<List<ProductModel>> = remember { mutableStateOf(listOf())}
    val currentTicketLines = remember { mutableStateOf(listOf<ProductModel>()) }

    if(currentTicketLines.value.isNotEmpty()){
        afterFirstProduct.value = false
        for(line in currentTicketLines.value)
            Logger.d(" $line \n $currentTable \n $currentTicket ")
    }

    //TODO otro boolean para el reset
    if(toReset){
        currentTicketLines.value = listOf()
        tableViewModel.resetTableViewModel()
        toReset = false
    }

    if(currentTicketLines.value.isEmpty() || firstOpenTable){
        tableViewModel.recoverTable(currentTable._id, currentTicketLines, productClicked)
        Logger.wtf(currentTicketLines.value.toString())
        Logger.wtf(currentTable._id + currentTable.name)
        Logger.wtf("Ticket" + currentTable.id_ticket)
       // currentTicketLines.value = tableViewModel.ticketLinesResponse
        if(currentTicketLines.value.isNotEmpty()){
            productClicked.value = true
            isComensalesOpen.value = false
        }else{
            if(!currentTable.ocupada && firstOpenTable)
                isComensalesOpen.value = true
        }
        firstOpenTable = false
    }

    title.value = "${currentTable.name} - ${currentUser.name}"

    if(productClicked.value){
        recalculate(
            currentTicketLines = currentTicketLines,
            totalBill = totalBill,
            tableViewModel = tableViewModel
        )
        productClicked.value = false
    }

    tableViewModel.getClientFamilies(currentClient._id)

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
        totalBill = totalBill,
        applicationContext = applicationContext,
        isComensalesOpen = isComensalesOpen,
        isCobrarOpen = isCobrarOpen,
        isLineOptionsOpen = isLineOptionsOpen,
        currentLine = currentLine
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

    if (isComensalesOpen.value)
        ShowAlertDialogComensales(
            isComensalesOpen = isComensalesOpen,
            tableViewModel = tableViewModel,
            applicationContext = applicationContext
        )

    if(isCobrarOpen.value)
        ShowAlertDialogCobrar(
            isCobrarOpen = isCobrarOpen,
            tableViewModel = tableViewModel,
            applicationContext = applicationContext,
            navController = navController,
            scaffoldState = scaffoldState,
            scope = scope
        )
    
    if(isLineOptionsOpen.value){
        ShowAlertDialogLineOptions(
            isLineOptionsOpen = isLineOptionsOpen,
            currentLine = currentLine,
            tableViewModel = tableViewModel,
            applicationContext = applicationContext,
            scope = scope,
            currentTicketLines = currentTicketLines
        )
    }
}


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
    applicationContext: Context,
    isComensalesOpen: MutableState<Boolean>,
    isCobrarOpen: MutableState<Boolean>,
    isLineOptionsOpen: MutableState<Boolean>,
    currentLine: MutableState<ProductModel>,
) {
    allFamilies = tableViewModel.clientFamiliesResponse

    Scaffold(
        scaffoldState = scaffoldState,
        snackbarHost = {
            SnackbarHost(
                hostState = snackbarHostState,
                snackbar = {
                    Card(
                        modifier = Modifier
                            .fillMaxWidth()
                            .clickable { snackbarHostState.currentSnackbarData?.dismiss() },
                        backgroundColor = colorResource(id = R.color.oscuro)
                    ){
                        Column(
                            horizontalAlignment = Alignment.CenterHorizontally,
                            verticalArrangement = Arrangement.Center,
                            modifier = Modifier.fillMaxWidth()
                        ) {
                            Row(
                                modifier = Modifier.fillMaxWidth()
                            ){
                                Text(
                                    text = currentLine.value.comentario,
                                    color = Color.White,
                                    fontWeight = FontWeight.Bold,
                                    modifier = Modifier.padding(30.dp).fillMaxWidth(0.8f)
                                )
                                IconButton(
                                    //modifier = Modifier.size(20.dp).padding(20.dp),
                                    onClick = { snackbarHostState.currentSnackbarData?.dismiss() }
                                ) {
                                    Icon(Icons.Filled.HighlightOff, contentDescription = "Ver comentario", tint = Color.White)
                                }
                            }
                        }
                    }
                }
            )},
        drawerBackgroundColor = colorResource(id = R.color.gris_muy_claro),
        drawerShape = customShape(),
        drawerContent =  { OptionsComponent(isCobrarOpen, isComensalesOpen) },
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
                    IconButton(onClick = { clickCerrar(currentTicketLines, tableViewModel, navController) }
                    ) {
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
                FamiliesComponent(tableViewModel, scope, familyProductList, isDialogOpen)
            }

            TicketHeadComponent(totalBill = totalBill)

            if(!currentTicketLines.value.isNullOrEmpty()) {
                TicketContentComponent(
                    currentTicketLines,
                    applicationContext,
                    currentLine,
                    isLineOptionsOpen,
                    snackbarHostState,
                    scope
                )
            }
        }
    }
}

@Composable
fun customShape() = object : Shape {
    override fun createOutline(
        size: Size,
        layoutDirection: LayoutDirection,
        density: Density
    ): Outline {
        return Outline.Rectangle(Rect(left = 0f, top = 0f, right = size.width * 2 / 3, bottom = size.height))
    }
}


private fun clickCerrar(
    currentTicketLines: MutableState<List<ProductModel>>,
    tableViewModel: TableViewModel,
    navController: NavController
) {
    if (!currentTicketLines.value.isNullOrEmpty()) {
        currentTable.id_ticket = currentTicket._id
        currentTable.ocupada = true
        Logger.wtf("Mesa cerrada llena \nCurrentTicket ${currentTicket._id} && Table ${currentTable.id_ticket}")
        tableViewModel.updateTable(currentTable, currentTable._id)
        currentTicketLines.value = listOf()
    } else {
        if (currentTable.id_ticket != "Error")
            tableViewModel.deleteTicket(currentTable.id_ticket)
        currentTable.id_ticket = "Error"
        currentTable.ocupada = false
        tableViewModel.updateTable(currentTable, currentTable._id)
        Logger.d("Cerrar mesa vacia")
    }
    navController.navigate(ScreenNav.MapScreen.route) {
        popUpTo(navController.graph.findStartDestination().id) { saveState = false }
        restoreState = true
    }
    //   firstOpenTable = true
}

fun recalculate(
    currentTicketLines: MutableState<List<ProductModel>>,
    totalBill: MutableState<Float>,
    tableViewModel: TableViewModel
) {
    totalBill.value = 0.0f
    if (!currentTicketLines.value.isNullOrEmpty())
        for (line in currentTicketLines.value) {
            line.total = line.cantidad * line.precio
            totalBill.value = totalBill.value + line.total
        }
    currentTicket.total = totalBill.value
    currentTicket.id_user_que_abrio = currentUser._id
    tableViewModel.updateTicket(currentTicket, currentTicket._id)

}







