package com.hfad.beeradviser;

import androidx.activity.ComponentActivity;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;
import android.widget.Spinner;
import android.view.View;

import java.util.List;

public class FindBeerActivity extends ComponentActivity {
    private BeerExport export = new BeerExport();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_find_beer);
    }

    public void onClickFindBeer(View view){
        TextView brands = (TextView) findViewById(R.id.brands);
        Spinner color = (Spinner) findViewById(R.id.color);
        String beerType = String.valueOf(color.getSelectedItem());

        List<String> BrandsList = export.getBrands(beerType);
        StringBuilder BrandsFormatted = new StringBuilder();
        for(String brand : BrandsList){
            BrandsFormatted.append(brand).append('\n');
        }
        brands.setText(BrandsFormatted);
    }
}