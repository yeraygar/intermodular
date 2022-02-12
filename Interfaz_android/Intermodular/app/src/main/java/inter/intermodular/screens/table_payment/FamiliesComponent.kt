package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.material.Button
import androidx.compose.material.ButtonDefaults
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.models.ProductModel
import inter.intermodular.support.allFamilies
import inter.intermodular.support.currentFamily
import inter.intermodular.support.currentProductList
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun FamiliesComponent(
    tableViewModel: TableViewModel,
    scope: CoroutineScope,
    familyProductList: MutableState<List<ProductModel>>,
    isDialogOpen: MutableState<Boolean>
) {
    LazyVerticalGrid(
        cells = GridCells.Adaptive(100.dp),
        modifier = Modifier.fillMaxSize()
    ) {
        for (i in 0 until tableViewModel.clientFamiliesResponse.count()) {
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

                        scope.launch {
                            tableViewModel.getFamilyProducts(tableViewModel.clientFamiliesResponse[i]._id)
                            delay(100)
                            familyProductList.value = tableViewModel.familyProductsResponse

                            currentProductList = tableViewModel.familyProductsResponse
                            isDialogOpen.value = true

                        }

                    }) {
                    Text(
                        text = if (allFamilies[i].name.length > 5)
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