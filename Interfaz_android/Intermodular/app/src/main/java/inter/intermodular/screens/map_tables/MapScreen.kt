package inter.intermodular.screens.map_tables

import androidx.compose.foundation.*
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.orhanobut.logger.Logger
import inter.intermodular.R
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

    var title = remember { mutableStateOf("")}

    if(!mapViewModel.clientZonesResponse.isNullOrEmpty()){
        if(firstOpenMap){
            title.value = mapViewModel.clientZonesResponse[0].zone_name ?: "Error de Carga"
            currentZone = mapViewModel.clientZonesResponse[0]
            firstOpenMap = false
        }
        mapViewModel.getZoneTables(currentZone!!._id)
        if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
            currentZoneTables = mapViewModel.zoneTablesResponse
            MapTablesStart(
                title = title,
                scaffoldState = scaffoldState,
                scope = scope,
                mapViewModel = mapViewModel,
                snackbarHostState = snackbarHostState,
                navController = navController
            )
        }
    }
}


@OptIn(ExperimentalFoundationApi::class)
@Composable
fun MapTablesStart(
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
                    SnackBarContent(snackbarHostState, navController)
                }
            )
        },

        drawerShape = MaterialTheme.shapes.small,
        drawerContent = {
            MapZoneComponent(mapViewModel, title, scope, scaffoldState)
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
                        Icon(Icons.Filled.Menu, contentDescription = null)
                    }
                },
                actions = {
                    IconButton(onClick = {
                        scope.launch { snackbarHostState.showSnackbar("hola") }
                    }) {
                        Icon(Icons.Filled.Settings, contentDescription = "Localized description")
                    }
                }
            )
        }
    ) {
        MapTableComponent(mapViewModel, navController)
    }
}










