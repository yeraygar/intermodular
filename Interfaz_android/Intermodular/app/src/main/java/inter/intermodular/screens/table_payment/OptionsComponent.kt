package inter.intermodular.screens.table_payment

import androidx.compose.foundation.layout.*
import androidx.compose.material.Button
import androidx.compose.material.ButtonDefaults
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import inter.intermodular.R

@Composable
fun OptionsComponent(
    isCobrarOpen: MutableState<Boolean>,
    isComensalesOpen: MutableState<Boolean>
) {
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

            ) {
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

            ) {
            Text(
                text = "MOD. COMENSALES",
                fontWeight = FontWeight.ExtraBold,
                color = Color.White
            )
        }
    }
}