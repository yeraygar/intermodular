package inter.intermodular.screens.table_payment

import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.navigation.NavController
import inter.intermodular.support.currentTable
import inter.intermodular.view_models.TableViewModel

@Composable
fun TableScreen(navController: NavController, tableViewModel : TableViewModel){
    Text(text = "Actividad Mesa con la mesa $currentTable")



}