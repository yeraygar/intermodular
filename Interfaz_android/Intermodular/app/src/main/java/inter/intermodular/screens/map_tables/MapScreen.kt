package inter.intermodular.screens.map_tables

import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.alpha
import androidx.compose.ui.draw.clip
import androidx.compose.ui.draw.clipToBounds
import androidx.compose.ui.draw.shadow
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.support.currentZone
import inter.intermodular.support.currentZoneTables
import inter.intermodular.support.firstOpenMap
import inter.intermodular.view_models.MapViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch

@Composable
fun MapScreen(mapViewModel: MapViewModel, navController: NavHostController){

    mapViewModel.getClientZones("Ecosistema1")
    mapViewModel.getUsersFichados(true)
    mapViewModel.getClientUsersList()
    mapViewModel.getClientAdmins()

    var scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()
    var snackbarHostState = remember { SnackbarHostState()}


    var isMenuActive = remember { mutableStateOf(true)}
    var title = remember { mutableStateOf("")}

    //var zones : List<ZoneModel> = mapViewModel.clientZonesResponse
    //title.value = zones[0].zone_name
    if(!mapViewModel.clientZonesResponse.isNullOrEmpty()){
        if(firstOpenMap){
            title.value = mapViewModel.clientZonesResponse[0].zone_name ?: "Error de Carga"
            currentZone = mapViewModel.clientZonesResponse[0]
            firstOpenMap = false
        }
        mapViewModel.getZoneTables(currentZone!!._id)
        if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
            currentZoneTables = mapViewModel.zoneTablesResponse
            LoadTables(
                title = title,
                scaffoldState = scaffoldState,
                scope = scope,
                mapViewModel = mapViewModel,
                snackbarHostState = snackbarHostState,
                navController = navController
            )

        }
    }


    if (isMenuActive.value ){
        Logger.d("isMenuActive changed in $isMenuActive")
        //DropdownDemo()
    }
    Logger.d("isMenuActive changed out $isMenuActive")


    // mapViewModel.getClientZones(currentClient._id)








}



@OptIn(ExperimentalFoundationApi::class)
@Composable
fun LoadTables(
    title: MutableState<String>,
    scaffoldState: ScaffoldState,
    scope: CoroutineScope,
    mapViewModel: MapViewModel,
    snackbarHostState: SnackbarHostState,
    navController: NavHostController
) {
    Scaffold(
        scaffoldState = scaffoldState,
        snackbarHost = {
            SnackbarHost(
                hostState = snackbarHostState,
                snackbar = {
                    Card(
                        shape = RoundedCornerShape(50.dp),
                        //border = BorderStroke(2.dp, Color.White),

                        modifier = Modifier
                            .padding(16.dp)
                            .wrapContentSize()
                            .clipToBounds()
                    ) {
                        Column(){
                            //TODO encajar bien los botones
                            Spacer(modifier = Modifier.height(30.dp))

                            /**TODO Agregar ruta de opciones Admin si el currentUser rol es Admin*/
                            if(true/*currentUser.rol == "Admin"*/){
                                Box(
                                    Modifier
                                        .background(
                                            color = colorResource(id = R.color.azul_oscuro),
                                            RoundedCornerShape(15.dp)
                                        )
                                        .shadow(
                                            elevation = 5.dp,
                                            shape = RoundedCornerShape(15.dp),
                                            clip = false
                                        )
                                        .clickable {
                                            Logger.d("CLICK EN ADMIN")
                                            //navController.navigate(ScreenNav.LoginScreen.route)

                                        }
                                ){
                                    Column(
                                        modifier = Modifier
                                            .padding(8.dp)
                                            .fillMaxWidth()
                                            .height(150.dp),
                                        verticalArrangement = Arrangement.Center,
                                        horizontalAlignment = Alignment.CenterHorizontally
                                    ) {
                                        Icon(imageVector = Icons.Default.Settings, contentDescription = "")
                                        Text(text = "Admin Options")
                                    }
                                }

                                Spacer(modifier = Modifier.height(10.dp))
                            }

                            Box(
                                Modifier
                                    .background(
                                        color = colorResource(id = R.color.azul),
                                        RoundedCornerShape(15.dp)
                                    )
                                    .shadow(
                                        elevation = 5.dp,
                                        shape = RoundedCornerShape(15.dp),
                                        clip = false
                                    )
                                    .clickable {
                                        Logger.d("CLICK EN CERRAR")
                                        snackbarHostState.currentSnackbarData?.dismiss()
                                    }
                            ){
                                Column(
                                    modifier = Modifier
                                        .padding(8.dp)
                                        .fillMaxWidth()
                                        .height(150.dp),
                                    verticalArrangement = Arrangement.Center,
                                    horizontalAlignment = Alignment.CenterHorizontally
                                ) {
                                    Icon(imageVector = Icons.Default.ExitToApp, contentDescription = "")
                                    Text(text = "Cerrar")
                                }
                            }

                            Spacer(modifier = Modifier.height(10.dp))

                            Box(
                                Modifier
                                    .background(
                                        color = colorResource(id = R.color.rojo),
                                        RoundedCornerShape(15.dp)
                                    )
                                    .shadow(elevation = 3.dp, shape = RoundedCornerShape(15.dp,0.dp,0.dp,0.dp),clip = true)
                                    .clickable {
                                        Logger.d("CLICK EN LOGOUT")
                                        navController.navigate(ScreenNav.LoginScreen.route)

                                     }

                            ){
                                Column(
                                    modifier = Modifier
                                        .padding(8.dp)
                                        .fillMaxWidth()
                                        .height(150.dp),
                                    horizontalAlignment = Alignment.CenterHorizontally,
                                    verticalArrangement = Arrangement.Center,


                                ) {
                                    Icon(imageVector = Icons.Default.Logout, contentDescription = "")
                                    Text(text = "LogOut")
                                }
                            }
                            Spacer(modifier = Modifier.height(30.dp))


                        }
                    }
                }
            )
        },

        drawerShape = MaterialTheme.shapes.small,
        drawerContent = {


            LazyColumn(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.spacedBy(15.dp),
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(10.dp, 5.dp)
            ){
                items(mapViewModel.clientZonesResponse.count()) { i ->
                    Button(
                        onClick = {
                            currentZone = mapViewModel.clientZonesResponse[i]
                            mapViewModel.getZoneTables(mapViewModel.clientZonesResponse[i]._id)
                            currentZoneTables = mapViewModel.zoneTablesResponse
                            title.value = currentZone!!.zone_name
                            mapViewModel.getZoneTables(currentZone!!._id)
                            if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
                                currentZoneTables = mapViewModel.zoneTablesResponse
                                scope.launch {
                                    scaffoldState.drawerState.close()
                                }
                            }
                        },
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(50.dp)
                    ) {
                        Text(text = mapViewModel.clientZonesResponse[i].zone_name)

                    }
                }



            }

        },

        topBar = {
            TopAppBar(

                backgroundColor = colorResource(id = R.color.azul_oscuro),
                title = { Text(title.value, modifier = Modifier.padding(30.dp,0.dp,0.dp,0.dp), color = colorResource(
                    id = R.color.white
                )) },
                navigationIcon = {

                    IconButton(
                        onClick = {
                            Logger.d("Click en options icon")
                            scope.launch {
                                scaffoldState.drawerState.open()
                            }

                        },

                        ) {
                        Icon(Icons.Filled.Menu, contentDescription = null)


                    }

                },
                actions = {

                    IconButton(onClick = {
                        scope.launch {
                            snackbarHostState.showSnackbar("hola")

                        }
                    }) {
                        Icon(Icons.Filled.Settings, contentDescription = "Localized description")
                    }

                }

            )

        }

    ) {
        LazyVerticalGrid(
            cells = GridCells.Fixed(6),
           // contentPadding = PaddingValues(50.dp),


            modifier = Modifier
                .fillMaxSize()
                .padding(5.dp)
        ) {
            for(i in 0 until currentZoneTables.count()){
                item {
                    RowContent(i, mapViewModel)
                }

            }

        }
    }
}

@Composable
fun RowContent(i: Int, mapViewModel: MapViewModel) {

if(currentZone != null){
    mapViewModel.getZoneTables(currentZone!!._id)
    if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
        currentZoneTables = mapViewModel.zoneTablesResponse


        var countTables = currentZoneTables.count()
        Logger.i("Numero de Mesas cargadas en la zona: ${countTables.toString()}")


        Button(
            modifier = Modifier
                .height(75.dp)
                .width(75.dp)
                .padding(3.dp),
            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
            onClick = { /**TODO Asignar la currentMesa a la i y navegar a la vista de la mesa*/ }) {
                Text(text = currentZoneTables[i].name.substring(0,3), fontSize = 11.sp,
                )
            }

        }

    }
}



