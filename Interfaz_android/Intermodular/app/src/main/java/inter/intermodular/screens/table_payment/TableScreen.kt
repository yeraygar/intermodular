package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
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
import androidx.compose.ui.res.fontResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.models.ProductModel
import inter.intermodular.support.*
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import java.time.format.TextStyle

@Composable
fun TableScreen(navController: NavController, tableViewModel : TableViewModel){

    val scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()
    val snackbarHostState = remember { SnackbarHostState()}
    val title = remember { mutableStateOf("") }
    val isDialogOpen = remember { mutableStateOf(false)}
    val afterFirstProduct = remember { mutableStateOf(false)}

    var familyProductList : MutableState<List<ProductModel>> = remember { mutableStateOf(listOf())}
    var currentTicketLines : MutableState<MutableList<ProductModel>> = remember { mutableStateOf(mutableListOf())}

    if(currentTicketLines.value.isEmpty()){
        tableViewModel.hasOpenTicket(currentTable._id)
        if (!tableViewModel.openTicketResponse.isNullOrEmpty())
        if(currentTable._id == tableViewModel.openTicketResponse[0].id_table){
            currentTable.ocupada = true
            currentTicket = tableViewModel.openTicketResponse[0]
            tableViewModel.getTicketLines(currentTicket._id)
            if(!tableViewModel.ticketLinesResponse.isNullOrEmpty())
                currentTicketLines.value.addAll(0,tableViewModel.ticketLinesResponse)
        }
    }

    title.value = "${currentTable.name} - ${currentUser.name}"

    tableViewModel.getClientFamilies(currentClient._id)
    //(!tableViewModel.clientFamiliesResponse.isNullOrEmpty() && firstOpenTable){
        firstOpenTable = false
        TableStart(
            navController = navController,
            tableViewModel = tableViewModel,
            scaffoldState = scaffoldState,
            scope = scope,
            snackbarHostState = snackbarHostState,
            title = title,
            isDialogOpen = isDialogOpen,
            familyProductList = familyProductList,
            currentTicketLines = currentTicketLines
        )
    if (isDialogOpen.value)
        ShowAlertDialogFamilyProducts(
            isDialogOpen = isDialogOpen,
            tableViewModel = tableViewModel,
            scope = scope,
            familyProductList = familyProductList,
            currentTicketLines = currentTicketLines,
            afterFirstProduct = afterFirstProduct
        )
   // }
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
    currentTicketLines: MutableState<MutableList<ProductModel>>,
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
                        navController.navigate(ScreenNav.MapScreen.route)
                    }) {
                        Icon(Icons.Filled.HighlightOff, contentDescription = "Localized description", tint = Color.White)
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
            Card(
                backgroundColor = colorResource(id = R.color.gris_muy_claro),
                modifier = Modifier
                    .fillMaxWidth()
                    //.fillMaxHeight()
                    .padding(5.dp)
            ) {
                LazyVerticalGrid(
                    cells = GridCells.Fixed(5),
                    contentPadding = PaddingValues(5.dp),
                    //modifier = Modifier.padding(5.dp)
                ){
                    item (span = { GridItemSpan(5)}) {
                        Column ( modifier =  Modifier.fillMaxWidth(),
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
                                    text = "TOTAL CUENTA : ",
                                    modifier = Modifier
                                        .fillMaxSize()
                                        .padding(5.dp),
                                    fontWeight = FontWeight.ExtraBold,
                                    fontSize = 16.sp,
                                    color = colorResource(id = R.color.azul_oscuro)
                                )
                            }
                        }

                    }
                    item {

                        Column ( modifier =  Modifier.fillMaxWidth(),
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
                    item (span = { GridItemSpan(2)}) {
                        Column ( modifier =  Modifier.fillMaxWidth(),
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
                        Column ( modifier =  Modifier.fillMaxWidth(),
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
                        Column ( modifier =  Modifier.fillMaxWidth(),
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

            /*TODO MOSTRAR LINEAS TICKET*/
            if(!currentTicketLines.value.isNullOrEmpty()) {
                Card(
                    backgroundColor = colorResource(id = R.color.gris_muy_claro),
                    modifier = Modifier
                        .fillMaxWidth()
                        .fillMaxHeight(0.2f)
                        .padding(5.dp)
                ) {
                    LazyVerticalGrid(
                        cells = GridCells.Fixed(5),
                        horizontalArrangement = Arrangement.Center,
                        verticalArrangement = Arrangement.Center,
                        contentPadding = PaddingValues(5.dp),
                    ) {
                        for (i in 0 until currentTicketLines.value.count()) {
                            item {
                                Text(
                                    text = "${currentTicketLines.value[i].cantidad}",
                                    modifier = Modifier
                                        .fillMaxSize()
                                        .padding(10.dp)
                                )
                            }
                            item(span = { GridItemSpan(2) }) {
                                Text(
                                    text = currentTicketLines.value[i].name,
                                    modifier = Modifier
                                        .fillMaxSize()
                                        .padding(10.dp)
                                )
                            }
                            item {
                                Text(
                                    text = "${currentTicketLines.value[i].precio}",
                                    modifier = Modifier
                                        .fillMaxSize()
                                        .padding(10.dp)
                                )
                            }
                            item {
                                Text(
                                    text = "${currentTicketLines.value[i].total}",
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
    }
}


