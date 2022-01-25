package inter.intermodular.services

import inter.intermodular.models.UserModel
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Headers

interface ApiServices {

    @Headers("Accept: Application/json")

    @GET("api/users/client/Ecosistema1")
    suspend fun getClientUsers() : List<UserModel>
   // http://localhost:8081/api/users/client/Ecosistema1

    companion object {
        private var apiServices:ApiServices? = null

        fun getInstance(): ApiServices{
            //if(apiServices == null){
                apiServices = Retrofit.Builder()
                    //.baseUrl("http://127.0.0.1:8081/") //localhost
                    .baseUrl("http://192.168.56.1:8081/") //localhost
                    .addConverterFactory(GsonConverterFactory.create())
                    .build()
                    .create(ApiServices::class.java)
           // }
            return apiServices!!
        }
    }
}