package shifa.nasz.locally;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.util.Log;
import android.widget.Toast;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Nasz on 1/31/15.
 */
public class GPSTracker {
    private static final String TAG = "GPSService";

    public LocationManager locationManager;
    private String provider;
    public MyLocationListener mylistener;
    private Criteria criteria;
    protected  Context ctx;
    public   double geolat = 0;
    public  String SessionID="";
    public  double geolng = 0;
    protected String GPSSpy = "";

    @TargetApi(Build.VERSION_CODES.HONEYCOMB)
    @SuppressLint("NewApi")
    public  GPSTracker(Context ctx){
        this.ctx = ctx;
        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        SessionID = SLAc.GetPreferenceValue("ProfileMobile");

    }
    public void GPSActivate(){
        try {
            locationManager = (LocationManager) ctx.getSystemService(Context.LOCATION_SERVICE);
            // Define the criteria how to select the location provider
            criteria = new Criteria();
            criteria.setAccuracy(Criteria.ACCURACY_COARSE);   //default

            // user defines the criteria

            criteria.setCostAllowed(false);
            // get the best provider depending on the criteria
            provider = locationManager.getBestProvider(criteria, false);

            // the last known location of this provider
            Location location = locationManager.getLastKnownLocation(provider);

            mylistener = new MyLocationListener();


            if (GPSSpy.equalsIgnoreCase("yes") || true) {
                if (location != null) {

                    mylistener.onLocationChanged(location);
                    Super_Library_URL SLU1 = new Super_Library_URL(TAG + "-" + "LOCATION IS NOT NULL.. setting locationchanged function ", ((Activity) ctx));
                } else {
                    locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 3000, 10, mylistener);
                    Super_Library_URL SLU1 = new Super_Library_URL(TAG + "-" + "LOCATION IS  NULL.. request location update ", ((Activity) ctx));

                    // leads to the settings because there is no last known location
                   // Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                   // ctx.startActivity(intent);
                }
                // location updates: at least 1 meter and 200millsecs change
                //locationManager.requestLocationUpdates(provider, 200, 1, mylistener);
            }
            String a = "" + location.getLatitude() + " - " + location.getLongitude();
            geolat = location.getLatitude();
            geolng = location.getLongitude();
            Super_Library_URL SLU1 = new Super_Library_URL(TAG + "-" + "Lat Long received.. " + geolat + "," + geolng, ((Activity) ctx));

            if (TrackGPSIndicator() == true){

                ArrayList<NameValuePair> nameValuePairs =  new ArrayList<NameValuePair>(2);
                nameValuePairs.add(new BasicNameValuePair("mobile", SessionID));
                nameValuePairs.add(new BasicNameValuePair("geolat", String.valueOf(geolat)));
                nameValuePairs.add(new BasicNameValuePair("geolng", String.valueOf(geolng)));
                Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/gps.php", nameValuePairs, ((Activity) ctx));
            }
           // Toast.makeText(ctx.getApplicationContext(), a, 222).show();
        }
        catch (Exception ex){
            Log.e(TAG,"Error in GPSActivate function",ex);
            Super_Library_URL SLU = new Super_Library_URL(TAG + "-" + ex.toString(), ((Activity) ctx));
        }
        Toast.makeText(ctx, "Establishing Internet Connection....", 1000).show();
    }
    public boolean TrackGPSIndicator(){
        //Log.e(TAG,"Inside track gps indicatro");
        //     Log.e(TAG,"SessionID mobile "+SessionID);
        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        double oldlat = Double.parseDouble(SLAc.GetPreferenceValue("ProfileLat"));

        double oldlng = Double.parseDouble(SLAc.GetPreferenceValue("ProfileLng"));
        if (oldlat == geolat || oldlng == geolng){
            // Log.e(TAG,"Inside track gps Class " + GPS.geolat + " == " + GPS.geolng);
            // suppose a user sit a one place and so gps will not changed so no need to update in tthe database
            // make sure GPSTracker class reset the value when it's update.
            return false;
        }
        SLAc.SavePreference("ProfileLat",String.valueOf(geolat));
        SLAc.SavePreference("ProfileLng",String.valueOf(geolng));

        Log.e(TAG, "Inside track gps geolat" + geolat + " == geolng" + geolng);
        return true;
    }
    private class MyLocationListener implements LocationListener {



        @Override
        public void onLocationChanged(Location location) {
            // Initialize the location fields




            geolat = location.getLatitude();
            geolng = location.getLongitude();
            Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
            SLAc.SavePreference("ProfileLat",String.valueOf(geolat));
            SLAc.SavePreference("ProfileLng",String.valueOf(geolng));
            if (TrackGPSIndicator() == true){

                ArrayList<NameValuePair> nameValuePairs =  new ArrayList<NameValuePair>(2);
                nameValuePairs.add(new BasicNameValuePair("mobile", SessionID));
                nameValuePairs.add(new BasicNameValuePair("geolat", String.valueOf(geolat)));
                nameValuePairs.add(new BasicNameValuePair("geolng", String.valueOf(geolng)));
                Super_Library_URL SLU = new Super_Library_URL("http://kent.nasz.us/mumbra/php/gps.php", nameValuePairs, ((Activity) ctx));
            }


            Toast.makeText(ctx, "MyListner Lat lng: " + location.getLatitude() + location.getLongitude(),
                    Toast.LENGTH_SHORT).show();
            locationManager.removeUpdates(mylistener);
            locationManager = null;

        }

        @Override
        public void onStatusChanged(String provider, int status, Bundle extras) {
       //     Toast.makeText(ctx, provider + "'s status changed to " + status + "!",
         //           Toast.LENGTH_SHORT).show();
        }

        @Override
        public void onProviderEnabled(String provider) {
          //  Toast.makeText(ctx, "Provider " + provider + " enabled!",
          //          Toast.LENGTH_SHORT).show();

        }

        @Override
        public void onProviderDisabled(String provider) {
         //   Toast.makeText(ctx, "Provider " + provider + " disabled!",
           //         Toast.LENGTH_SHORT).show();
        }
    }
}


