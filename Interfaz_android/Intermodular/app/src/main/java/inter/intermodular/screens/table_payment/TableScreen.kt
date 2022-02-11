package inter.intermodular.screens.table_payment

import android.content.Context
import android.widget.Toast
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.combinedClickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.*
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
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
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.unit.Density
import androidx.compose.ui.unit.LayoutDirection
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
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
import java.lang.Exception
import java.util.*

@Composable
fun TableScreen(
    navController: NavController,
    tableViewModel: TableViewModel,
    applicationContext: Context
){
    val scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()
    val snackbarHostState = remember { SnackbarHostState()}
    val title = remember { mutableStateOf("") }
    val isDialogOpen = remember { mutableStateOf(false)}
    val afterFirstProduct = remember { mutableStateOf(false)}
    val totalBill = remember { mutableStateOf(0.0f)}
    val productClicked = remember { mutableStateOf(false)}

    val isComensalesOpen = remember { mutableStateOf(false)}
    val isCobrarOpen = remember { mutableStateOf(false)}

    var familyProductList : MutableState<List<ProductModel>> = remember { mutableStateOf(listOf())}
    val currentTicketLines = remember { mutableStateOf(listOf<ProductModel>()) }

    if(currentTicketLines.value.isNotEmpty()){
        afterFirstProduct.value = false
        for(line in currentTicketLines.value)
            Logger.d(" $line \n $currentTable \n $currentTicket ")
    }

    if(!currentTable.ocupada && currentTable.id_ticket != "Error"){
        //TODO PEDIR NUMERO COMENSALES, NUEVO DIALOG
    }

    //TODO otro boolean para el reset
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
        isCobrarOpen = isCobrarOpen
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
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun ShowAlertDialogCobrar(
    isCobrarOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    applicationContext: Context,
    navController: NavController,
    scaffoldState: ScaffoldState,
    scope: CoroutineScope
) {
    var aceptarEnabled = remember { mutableStateOf(false)}
    var cashOk = remember { mutableStateOf(false)}
    var pagoTarjeta = remember { mutableStateOf(false)}
    var cashInput = remember { mutableStateOf(0f)}

    cashOk.value = (cashInput.value >= currentTicket.total)
    if (pagoTarjeta.value) aceptarEnabled.value = true
    else aceptarEnabled.value = cashOk.value

    Dialog(onDismissRequest = { isCobrarOpen.value = false }) {
        Surface(
            modifier = Modifier
                //.width(400.dp)
                //.height(600.dp)
                .padding(10.dp),
            shape = RoundedCornerShape(5.dp),
            color = Color.White
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center,
                modifier = Modifier
                    // .fillMaxSize()
                    .padding(10.dp)
            ) {

                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp),
                    backgroundColor = colorResource(id = R.color.gris_muy_claro)
                ) {
                    Text(
                        text = "TOTAL MESA ${currentTable.name} :  ${currentTicket.total}€" ,
                        fontWeight = FontWeight.Bold,
                        fontSize = 16.sp,
                        modifier = Modifier.padding(10.dp))
                }

                Spacer(modifier = Modifier.height(5.dp))

                LazyVerticalGrid(cells = GridCells.Fixed(3), modifier = Modifier.fillMaxWidth(), contentPadding = PaddingValues(10.dp)){
                    item{
                        Card(
                            backgroundColor = if (pagoTarjeta.value) Color.White else colorResource(id = R.color.azul),
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(50.dp)
                        ){
                            Column (
                                horizontalAlignment = Alignment.CenterHorizontally,
                                verticalArrangement = Arrangement.Center,
                                modifier = Modifier.fillMaxWidth()
                            ){
                                Text(
                                    text = "EFECTIVO",
                                    modifier = Modifier.padding(5.dp),
                                    fontWeight = FontWeight.Bold,
                                    color = if (pagoTarjeta.value) Color.Black else Color.White
                                )
                            }
                        }
                    }
                    item{
                        Switch(
                            checked = pagoTarjeta.value,
                            onCheckedChange = { pagoTarjeta.value = it},
                            //modifier = Modifier.fillMaxWidth()
                        )
                    }
                    item{
                        Card(
                            backgroundColor = if (!pagoTarjeta.value) Color.White else colorResource(id = R.color.azul),
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(50.dp)
                        ){
                            Column (
                                horizontalAlignment = Alignment.CenterHorizontally,
                                verticalArrangement = Arrangement.Center,
                                modifier = Modifier.fillMaxWidth()
                            ){
                                Text(
                                    text = "TARJETA",
                                    modifier = Modifier.padding(5.dp),
                                    fontWeight = FontWeight.Bold,
                                    color = if (!pagoTarjeta.value) Color.Black else Color.White
                                )
                            }
                        }
                    }
                }


                OutlinedTextField(
                    enabled = !pagoTarjeta.value,
                    value = cashInput.value.toString(),
                    onValueChange = { it.also {
                        try{
                            cashInput.value = it.toFloat()

                        }catch (e: Exception){
                            Toast.makeText(applicationContext, "Caracteres Invalidos", Toast.LENGTH_SHORT).show()
                        }
                    } },
                    label = { Text(text = "Cantidad Introducida", style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)),) },
                    placeholder = { Text(text = "Cantidad Introducida",style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)),) },
                    singleLine = true,
                    keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                    trailingIcon = {
                        val image = Icons.Filled.Calculate
                        IconButton(onClick = { cashInput.value = currentTicket.total }
                        ) { Icon(imageVector  = image, "Dinero") }
                    },
                    colors = TextFieldDefaults.outlinedTextFieldColors(
                        focusedBorderColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedBorderColor = colorResource(id = R.color.gris_claro),
                        focusedLabelColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedLabelColor = colorResource(id = R.color.gris_claro),
                        cursorColor = colorResource(id = R.color.azul)
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp, 10.dp, 10.dp, 5.dp),
                )

                Spacer(modifier = Modifier.height(10.dp))

                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp),
                    backgroundColor = colorResource(id = R.color.gris_muy_claro)
                ) {
                    Text(
                        text = if (!pagoTarjeta.value) "A DEVOLVER: ${cashInput.value - currentTicket.total}€" else "",
                        fontWeight = FontWeight.Bold,
                        fontSize = 14.sp,
                        modifier = Modifier.padding(10.dp))
                }

                Spacer(modifier = Modifier.height(5.dp))

                Button(
                    enabled = aceptarEnabled.value,
                    onClick = {
                        if (!pagoTarjeta.value){
                            if (cashInput.value >= currentTicket.total){
                                currentTicket.cobrado = true
                                currentTicket.tipo_ticket = "Efectivo"
                                currentTicket.id_user_que_cerro = currentUser._id
                                currentTicket.date to Date()
                                currentTable.id_ticket = "Error"
                                currentTable.ocupada = false
                                tableViewModel.updateTicket(currentTicket, currentTicket._id)
                                tableViewModel.updateTable(currentTable, currentTable._id)
                                //firstOpenTable = true
                                navController.navigate(ScreenNav.MapScreen.route)

                            }else{
                                Toast.makeText(applicationContext, "Falta dinero en efectivo", Toast.LENGTH_SHORT).show()
                            }
                        }else{
                            currentTicket.tipo_ticket = "Tarjeta"
                            currentTicket.cobrado = true
                            currentTable.id_ticket = "Error"
                            currentTable.ocupada = false
                            currentTicket.id_user_que_cerro = currentUser._id
                            currentTicket.date to Date()
                            tableViewModel.updateTicket(currentTicket, currentTicket._id)
                            tableViewModel.updateTable(currentTable, currentTable._id)
                            // firstOpenTable = true
                            navController.navigate(ScreenNav.MapScreen.route)
                        }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(80.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                ) {
                    Text(
                        text = "COBRAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }
                Spacer(modifier = Modifier.height(10.dp))

                Button(
                    onClick = {
                        isCobrarOpen.value = false
                        scope.launch { scaffoldState.drawerState.close() }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(80.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.rojo)),
                ) {
                    Text(
                        text = "CANCELAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }
            }
        }
    }
}


@Composable
fun ShowAlertDialogComensales(
    isComensalesOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    applicationContext: Context
) {
    var comensales  = remember { mutableStateOf(0) }
    var aceptarEnabled = remember { mutableStateOf(false)}
    if (comensales.value <= currentTable.comensalesMax && comensales.value > 0){
        currentTable.comensales = comensales.value
        aceptarEnabled.value = true
    }
    else {
        Toast.makeText(applicationContext, "Comensales entre [1 - ${currentTable.comensalesMax}]", Toast.LENGTH_SHORT).show()
    }

    Dialog(onDismissRequest = { isComensalesOpen.value = false }) {
        Surface(
            modifier = Modifier
                //.fillMaxSize()
                .padding(10.dp, 20.dp),
            shape = RoundedCornerShape(5.dp),
            color = Color.White
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center
            ) {

                OutlinedTextField(
                    value = comensales.value.toString(),
                    onValueChange = { it.also {
                        try{
                            comensales.value = it.toInt()

                        }catch (e: Exception){
                            Toast.makeText(applicationContext, "Caracteres Invalidos", Toast.LENGTH_SHORT).show()
                        }
                    } },
                    label = { Text(text = "Numero de Comensales", style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)),) },
                    placeholder = { Text(text = "Numero de Comensales",style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)),) },
                    singleLine = true,
                    keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                    trailingIcon = {
                        val image = Icons.Filled.Groups
                        IconButton(onClick = {  }
                        ) { Icon(imageVector  = image, "Comensales") }
                    },
                    colors = TextFieldDefaults.outlinedTextFieldColors(
                        focusedBorderColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedBorderColor = colorResource(id = R.color.gris_claro),
                        focusedLabelColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedLabelColor = colorResource(id = R.color.gris_claro),
                        cursorColor = colorResource(id = R.color.azul)
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp, 10.dp, 10.dp, 5.dp),
                )

                Spacer(modifier = Modifier.height(5.dp))


                    Button(
                        enabled = aceptarEnabled.value,
                        onClick = {
                            if (comensales.value <= currentTable.comensalesMax && comensales.value > 0) {
                                isComensalesOpen.value = false
                                aceptarEnabled.value = false
                                tableViewModel.updateTable(currentTable, currentTable._id)
                            }else{
                                Toast.makeText(applicationContext, "Valor Incorrecto [1 - ${currentTable.comensalesMax}] ", Toast.LENGTH_SHORT).show()
                            }
                        },
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(60.dp)
                            .padding(10.dp),
                        shape = RoundedCornerShape(5.dp),
                        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                    ) {
                        Text(
                            text = "ACEPTAR",
                            color = Color.White,
                            fontSize = 12.sp
                        )
                    }

                Spacer(modifier = Modifier.height(5.dp))

                Button(
                      //  enabled = isAceptarEnabled.value,
                        onClick = {
                            isComensalesOpen.value = false
                        },
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(60.dp)
                            .padding(10.dp),
                        shape = RoundedCornerShape(5.dp),
                        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.rojo)),
                    ) {
                        Text(
                            text = "CANCELAR",
                            color = Color.White,
                            fontSize = 12.sp
                        )
                    }


            }
        }
    }
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
    applicationContext: Context,
    isComensalesOpen: MutableState<Boolean>,
    isCobrarOpen: MutableState<Boolean>,
) {

    allFamilies = tableViewModel.clientFamiliesResponse

    Scaffold(

        scaffoldState = scaffoldState,
        snackbarHost = {
            SnackbarHost(
                hostState = snackbarHostState,
                snackbar = {
                    Text(text = "aqui el grueso")

                    //No hara falta snackBar en este scaffold
                }
            )
        },
        drawerBackgroundColor = colorResource(id = R.color.gris_muy_claro),
        drawerShape = customShape(),
        drawerContent =  {

          Column(
              horizontalAlignment = Alignment.Start,
              verticalArrangement = Arrangement.Center,
              modifier = Modifier
                  .fillMaxSize()
                  //.padding(10.dp)
                  //.height(50.dp)
          ) {
              Spacer(modifier = Modifier.height(30.dp))

              Button(
                  onClick = {
                      isCobrarOpen.value = true
                  },
                  modifier = Modifier
                      // .fillMaxWidth()
                      .height(50.dp)
                      .defaultMinSize(210.dp, 5.dp),
                  colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),

                  ){
                  Text(
                      text = "COBRAR",
                      fontWeight = FontWeight.ExtraBold,
                      color = Color.White
                  )
              }

              Spacer(modifier = Modifier.height(30.dp))

              Button(
                  onClick = {
                      isComensalesOpen.value = true
                  },
                  modifier = Modifier
                      //.fillMaxWidth()
                      .height(50.dp)
                      .defaultMinSize(210.dp, 5.dp),
                  colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),

                  ){
                  Text(
                      text = "MOD. COMENSALES",
                      fontWeight = FontWeight.ExtraBold,
                      color = Color.White
                  )
              }
          }

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
                            currentTable.ocupada = true
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
                TicketContentComponent(currentTicketLines, applicationContext)
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

@OptIn(ExperimentalFoundationApi::class)
@Composable
private fun TicketContentComponent(
    currentTicketLines: MutableState<List<ProductModel>>,
    applicationContext: Context
) {
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
                        modifier = Modifier.combinedClickable(
                            onLongClick = {
                                Logger.wtf("LONG CLICK!!")
                        },
                            onClick = {
                                Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                            }
                        )
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
                        modifier = Modifier.combinedClickable(
                            onLongClick = {
                                Logger.wtf("LONG CLICK!!")
                            },
                            onClick = {
                                Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                            }
                        )
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
                        modifier = Modifier.combinedClickable(
                            onLongClick = {
                                Logger.wtf("LONG CLICK!!")
                            },
                            onClick = {
                                Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                            }
                        )
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
                        modifier = Modifier.combinedClickable(
                            onLongClick = {
                                Logger.wtf("LONG CLICK!!")
                            },
                            onClick = {
                                Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                            }
                        )
                    ){
                        Text(
                            text =
                            if (currentTicketLines.value[i].total.toString().length >= 5)
                                "${currentTicketLines.value[i].total.toString().substring(0,4).toFloat()}€"
                            else "${currentTicketLines.value[i].total}€",
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


