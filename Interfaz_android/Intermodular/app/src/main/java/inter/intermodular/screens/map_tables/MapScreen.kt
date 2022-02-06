package inter.intermodular.screens.map_tables

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.res.dimensionResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import inter.intermodular.R
import inter.intermodular.view_models.LoginRegisterViewModel
import inter.intermodular.view_models.MapViewModel

@Composable
fun MapScreen(mapViewModel : MapViewModel){



    Surface(color = MaterialTheme.colors.background) {

        mapViewModel.getUsersFichados(true)
        mapViewModel.getClientUsersList()
        mapViewModel.getClientAdmins()

        LoadTables()









        /*val data = listOf("Item 1", "Item 2", "Item 3", "Item 4", "Item 5")

        LazyVerticalGrid(
            cells = GridCells.Fixed(3),
            contentPadding = PaddingValues(8.dp)
        ) {
            items(data) { item ->
                Card(
                    modifier = Modifier.padding(4.dp),
                    backgroundColor = Color.LightGray
                ) {
                    Text(
                        text = item,
                        fontSize = 24.sp,
                        textAlign = TextAlign.Center,
                        modifier = Modifier.padding(24.dp)
                    )
                }
            }
        }*/

       /* Box(
            modifier = Modifier
                .fillMaxSize()

        ){
            Column() {
                Spacer(modifier = Modifier.height(18.dp))
                Text(text = email ?: "No se ha cargado bien")
                Spacer(modifier = Modifier.height(18.dp))
                Text(text = currentClient.email)
                //AllUsersClient(clientViewModel)
            }
        }*/
    }
}

/*
@Composable
fun AllUsersClient(clientViewModel: ClientViewModel : UserViewModel) {
    userViewModel.getClientUsersList()
    var lista : List<UserModel> = userViewModel.allUsersClientResponse

    Column(

    ){
        var usuarioMostrar : String = "Error";
        for(u: UserModel in lista){
            usuarioMostrar = u.name
            Text(text = "Hello $usuarioMostrar!")
        }
    }
}*/

@Composable
fun LoadTables() {
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
               modifier = Modifier.weight(1f).fillMaxSize(),
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
    LoadTables()
}



