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
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.support.currentClient
import inter.intermodular.view_models.MapViewModel

@Composable
fun MapScreen(mapViewModel : MapViewModel){




        var isMenuActive = remember { mutableStateOf(true)}
        if (isMenuActive.value ){
            Logger.d("isMenuActive changed in $isMenuActive")
            DropdownDemo()
        }
    Logger.d("isMenuActive changed out $isMenuActive")



    mapViewModel.getUsersFichados(true)
        mapViewModel.getClientUsersList()
        mapViewModel.getClientAdmins()
       // mapViewModel.getClientZones(currentClient._id)
        mapViewModel.getClientZones("Ecosistema1")

    LoadTables(isMenuActive)






}



@Composable
fun LoadTables(isMenuActive: MutableState<Boolean>) {
    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(currentClient.name, modifier = Modifier.padding(30.dp,0.dp,0.dp,0.dp)) },
                navigationIcon = {

                    IconButton(
                        onClick = {
                            isMenuActive.value = true
                            Logger.d("Click en options icon")

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
            for(i in 1..30){
                item {
                    RowContent()
                }
            }

        }
    }
}

@Composable
fun RowContent() {

       Row(

           horizontalArrangement = Arrangement.spacedBy(15.dp),

           modifier = Modifier
               .fillMaxWidth()
               .padding(5.dp)
               //.background(color = Color.Red)
               .height(50.dp)

       ){
           for(i in 0..5)
           Button(
               modifier = Modifier
                   .weight(1f)
                   .fillMaxSize(),
               colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(R.color.azul)),
               onClick = { /*TODO*/ }) {
               Text(text = "${i + 1}")
           }
       }

}

@Preview(
    name = "table",
    showBackground = true
)
@Composable
fun PreviewTable(){
    /*var isMenuActive = remember { mutableStateOf(false)}
    if (isMenuActive.value ) DropdownDemo()
    LoadTables(isMenuActive)*/
    DropdownDemo()
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




