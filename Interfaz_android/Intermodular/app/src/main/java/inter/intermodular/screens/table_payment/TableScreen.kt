package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.*
import androidx.compose.foundation.shape.RoundedCornerShape
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
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
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

@Composable
fun TableScreen(navController: NavController, tableViewModel : TableViewModel){

    currentLinesTicket = listOf()

    //TODO comprobacion de que la mesa esta vacia y en su defecto cargar las currentLinesTicket

    val scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()
    val snackbarHostState = remember { SnackbarHostState()}
    val title = remember { mutableStateOf("") }
    val isDialogOpen = remember { mutableStateOf(false)}

    var ticketLines : MutableState<List<ProductModel>> = remember { mutableStateOf(listOf())}


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
            ticketLines = ticketLines
        )

   /* ShowAlertDialogFamilyProducts(
        isDialogOpen = isDialogOpen,
        tableViewModel = tableViewModel,
        navController = navController,
        ticketLines = ticketLines
    )*/
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
    ticketLines: MutableState<List<ProductModel>>,
) {
    ShowAlertDialogFamilyProducts(
        isDialogOpen = isDialogOpen,
        tableViewModel = tableViewModel,
        navController = navController,
        ticketLines = ticketLines
    )
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
                backgroundColor = colorResource(id = R.color.azul),
                modifier = Modifier
                    .fillMaxWidth()
                    .fillMaxHeight(0.4f)
                    .padding(5.dp)
            ) {
                LazyVerticalGrid(
                    cells = GridCells.Adaptive(100.dp),
                    modifier = Modifier
                        .fillMaxSize()
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
                                        delay(1000)
                                        tableViewModel.getFamilyProducts(tableViewModel.clientFamiliesResponse[i]._id)
                                        delay(3000)
                                       ticketLines.value = tableViewModel.familyProductsResponse

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
                backgroundColor = colorResource(id = R.color.rojo),
                modifier = Modifier
                    .fillMaxWidth()
                    .fillMaxHeight()
                    .padding(5.dp)
            ) {
                LazyVerticalGrid(
                    cells = GridCells.Fixed(4),
                    //modifier = Modifier.fillMaxSize()
                ){
                    item(span =   { GridItemSpan(4)}
                    ){
                        Column ( modifier =  Modifier.fillMaxWidth(), horizontalAlignment = Alignment.End, verticalArrangement = Arrangement.Center) {
                            Text(text = "TOTAL CUENTA => ", modifier = Modifier.padding(10.dp))
                        }
                    }
                    item {
                        Text(text = "CANT.", modifier = Modifier
                            .fillMaxSize()
                            .padding(5.dp))
                    }
                    item {
                        Text(text = "NOMBRE", modifier = Modifier
                            .fillMaxSize()
                            .padding(5.dp))
                    }
                    item {
                        Text(text = "PRECIO", modifier = Modifier
                            .fillMaxSize()
                            .padding(5.dp))
                    }
                    item {
                        Text(text = "TOTAL", modifier = Modifier
                            .fillMaxSize()
                            .padding(5.dp))
                    }
                }
                /*TODO MOSTRAR LINEAS TICKET*/
               /* if(isDialogOpen.value){
                    if(!currentProductList.isNullOrEmpty()){
                        LazyVerticalGrid(cells = GridCells.Fixed(4)){
                            for(i in 0 until currentProductList.count()){
                                item {
                                    Text(text = "${ticketLines.value[i].cantidad}")
                                }
                                item {
                                    Text(text = ticketLines.value[i].name)
                                }
                                item {
                                    Text(text = "${ticketLines.value[i].precio}")
                                }
                                item {
                                    Text(text = "${ticketLines.value[i].total}")
                                }
                            }
                        }
                    }
                }*/
            }
        }
    }
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun ShowAlertDialogFamilyProducts(
    isDialogOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    navController: NavController,
    ticketLines: MutableState<List<ProductModel>>,
) {
    currentProductList = tableViewModel.familyProductsResponse

    if(isDialogOpen.value) {
        if (!ticketLines.value.isNullOrEmpty()){
            for ( i in 0 until ticketLines.value.count()){
                Logger.wtf(ticketLines.value[i].name)
                Dialog(onDismissRequest = { isDialogOpen.value = false }) {
                    Surface(
                        modifier = Modifier
                            .fillMaxSize()
                            .padding(10.dp, 20.dp),
                        shape = RoundedCornerShape(5.dp),
                        color = Color.White
                    ) {
                        LazyVerticalGrid(
                            cells = GridCells.Adaptive(10.dp),
                            modifier = Modifier
                                .fillMaxSize()
                        ){
                            item {
                                Button(
                                    modifier = Modifier
                                        .height(80.dp)
                                        .width(100.dp)
                                        .padding(5.dp),

                                    colors = ButtonDefaults.buttonColors(colorResource(id = R.color.azul_oscuro)),
                                    onClick = {
                                        currentProduct = ticketLines.value[i]
                                        Logger.i("Producto seleccionado $currentProduct")
                                        //TODO, logica de crear ticket y anyadir linea_ticket

                                        isDialogOpen.value = false

                                    }) {
                                    Text(
                                        text =  if (ticketLines.value[i].name.length > 5)
                                                     ticketLines.value[i].name.substring(0, 5)
                                                 else ticketLines.value[i].name,
                                        fontSize = 14.sp,
                                        color = Color.White,
                                        fontWeight = FontWeight.Bold
                                    )
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
