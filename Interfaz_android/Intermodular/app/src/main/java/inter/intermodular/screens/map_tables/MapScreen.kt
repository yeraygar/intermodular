package inter.intermodular.screens.map_tables

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Settings
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.models.TableModel
import inter.intermodular.support.currentZone
import inter.intermodular.support.currentZoneTables
import inter.intermodular.support.firstOpenMap
import inter.intermodular.view_models.MapViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch

@Composable
fun MapScreen(mapViewModel : MapViewModel){

    mapViewModel.getClientZones("Ecosistema1")
    mapViewModel.getUsersFichados(true)
    mapViewModel.getClientUsersList()
    mapViewModel.getClientAdmins()

    var scaffoldState = rememberScaffoldState()
    val scope = rememberCoroutineScope()


    var isMenuActive = remember { mutableStateOf(true)}
    var title = remember { mutableStateOf("")}

    var tablesToShow : List<TableModel> = remember { mutableListOf() }
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
            tablesToShow = mapViewModel.zoneTablesResponse
            LoadTables(title, scaffoldState, scope, mapViewModel,tablesToShow)

        }
    }


    if (isMenuActive.value ){
        Logger.d("isMenuActive changed in $isMenuActive")
        //DropdownDemo()
    }
    Logger.d("isMenuActive changed out $isMenuActive")


    // mapViewModel.getClientZones(currentClient._id)








}



@Composable
fun LoadTables(
    title: MutableState<String>,
    scaffoldState: ScaffoldState,
    scope: CoroutineScope,
    mapViewModel: MapViewModel,
    tablesToShow: List<TableModel>
) {
    Scaffold(
        scaffoldState = scaffoldState,
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
                title = { Text(title.value, modifier = Modifier.padding(30.dp,0.dp,0.dp,0.dp)) },
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

                    IconButton(onClick = { /* doSomething() */ }) {
                        Icon(Icons.Filled.Settings, contentDescription = "Localized description")
                    }

                }

            )

        }

    ) {
        LazyColumn(
            modifier = Modifier
                .fillMaxSize()
        ) {
            for(i in 0..tablesToShow.count()){
                item {
                    RowContent(tablesToShow, mapViewModel)
                }
            }

        }
    }
}

@Composable
fun RowContent(tablesToShow: List<TableModel>, mapViewModel: MapViewModel) {

if(currentZone != null){
    mapViewModel.getZoneTables(currentZone!!._id)
    if(!mapViewModel.zoneTablesResponse.isNullOrEmpty()){
        currentZoneTables = mapViewModel.zoneTablesResponse
        for (table in currentZoneTables) {
            if (tablesToShow.indexOf(table) % 6 != 0) {
                Button(
                    modifier = Modifier
                        .fillMaxSize(),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
                    onClick = { /*TODO*/ }) {
                    Text(text = "${table.name}")
                }


            } else {
                Row(

                    horizontalArrangement = Arrangement.spacedBy(15.dp),

                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(5.dp)
                        //.background(color = Color.Red)
                        .height(50.dp)

                ) {


                    Button(
                        modifier = Modifier
                            .weight(1f)
                            .fillMaxSize(),
                        colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
                        onClick = { /*TODO*/ }) {
                        Text(text = "${table.name}")
                    }
                }
            }

        }
    }
}



  /* Row(

       horizontalArrangement = Arrangement.spacedBy(15.dp),

       modifier = Modifier
           .fillMaxWidth()
           .padding(5.dp)
           //.background(color = Color.Red)
           .height(50.dp)

   ){
       for (table in tablesToShow){

       }
       *//*for(i in 0..5){
           Button(
               modifier = Modifier
                   .weight(1f)
                   .fillMaxSize(),
               colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
               onClick = { *//**//*TODO*//**//* }) {
               Text(text = "${i + 1}")
           }
       }*//*
   }*/

}



@Composable
fun DropdownDemo() {
    var expanded by remember { mutableStateOf(false) }
    val items = listOf(
        "Apple", "Banana", "Cherry", "Grapes",
        "Mango", "Pineapple", "Pear"
    )
    var selectedIndex by remember { mutableStateOf(0) }
    Column(
        modifier = Modifier
            .fillMaxSize()
            .wrapContentSize(Alignment.TopStart)
            .padding(all = 5.dp)
    ) {
        Text(
            items[selectedIndex],
            modifier = Modifier
                .fillMaxWidth()
                .clickable(onClick = { expanded = true })
                .background(
                    Color.Red
                ),
            color = Color.White,
            fontSize = 20.sp,
            textAlign = TextAlign.Start
        )
        DropdownMenu(
            expanded = expanded,
            onDismissRequest = { expanded = false },
            modifier = Modifier
                .fillMaxWidth()
                .background(
                    Color.Gray
                )
        ) {
            items.forEachIndexed { index, s ->
                DropdownMenuItem(onClick = {
                    selectedIndex = index
                    expanded = false
                }) {
                    Text(text = s)
                }
            }
        }
    }
}




