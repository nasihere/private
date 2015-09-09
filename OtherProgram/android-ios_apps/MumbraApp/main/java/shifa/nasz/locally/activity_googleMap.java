package shifa.nasz.locally;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.util.Log;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

public class activity_googleMap extends FragmentActivity {

    private GoogleMap mMap; // Might be null if Google Play services APK is not available.
    private static String TAG = "GoogleMapActivity";
    String[] oldMapData = null;
    Context ctx;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_activity_google_map);
        ctx =this;
        setUpMapIfNeeded();
    }

    @Override
    protected void onResume() {
        super.onResume();
        setUpMapIfNeeded();
    }

    /**
     * Sets up the map if it is possible to do so (i.e., the Google Play services APK is correctly
     * installed) and the map has not already been instantiated.. This will ensure that we only ever
     * call {@link #setUpMap()} once when {@link #mMap} is not null.
     * <p/>
     * If it isn't installed {@link SupportMapFragment} (and
     * {@link com.google.android.gms.maps.MapView MapView}) will show a prompt for the user to
     * install/update the Google Play services APK on their device.
     * <p/>
     * A user can return to this FragmentActivity after following the prompt and correctly
     * installing/updating/enabling the Google Play services. Since the FragmentActivity may not
     * have been completely destroyed during this process (it is likely that it would only be
     * stopped or paused), {@link #onCreate(Bundle)} may not be called again so we should call this
     * method in {@link #onResume()} to guarantee that it will be called.
     */
    private void setUpMapIfNeeded() {
        // Do a null check to confirm that we have not already instantiated the map.
        if (mMap == null) {
            // Try to obtain the map from the SupportMapFragment.
            mMap = ((SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map))
                    .getMap();
            // Check if we were successful in obtaining the map.
            if (mMap != null) {
                setUpMap();
            }
        }
    }
    @Override
    public void onStart()
    {
        super.onStart();
        setUpMap();
    }
    /**
     * This is where we can add markers or lines, add listeners or move the camera. In this case, we
     * just add a marker near Africa.
     * <p/>
     * This should only be called once and when we are sure that {@link #mMap} is not null.
     */
    private void setUpMap() {
        try {
            Super_Library_AppClass SLAc = new Super_Library_AppClass(this);
            String[] MapData = SLAc.GetArrayPreference("MapDataArray");
            if (oldMapData == null || oldMapData.equals(MapData)) return;
            oldMapData = MapData;
            boolean FirstMapFound = false;
            LatLng MOUNTAIN_VIEW = null;// = new LatLng(37.4, -122.1);
            for (int i = 0; i <= MapData.length - 1; i++) {
                Log.e(TAG, "MapData " + i + ": " + MapData[i]);
                if (MapData[i] != null) {
                    String[] Data = MapData[i].split("#-#");
                    //Data[0] is name and titile
                    //Data[1] is latlng
                    //data[2] is newrem where all address and details
                    String NameTitle = "MapItem";
                    if (Data[0]!=null) {
                        NameTitle = Data[0];
                    }
                    String[] lat_lng = {"19.17362849","73.02498129"};
                    if (Data[1] != null) {
                        lat_lng = Data[1].split(",");
                    }
                    if (FirstMapFound == false) {
                        FirstMapFound = true;
                        MOUNTAIN_VIEW = new LatLng(Double.parseDouble(lat_lng[0]), Double.parseDouble(lat_lng[1]));
                    }
                    mMap.addMarker(new MarkerOptions().position(new LatLng(Double.parseDouble(lat_lng[0]), Double.parseDouble(lat_lng[1]))).title(NameTitle));
                }
            }
            if (MapData.length > 1) {
                // Zoom in, animating the camera.
                mMap.animateCamera(CameraUpdateFactory.zoomIn());

// Zoom out to zoom level 10, animating with a duration of 2 seconds.
                mMap.animateCamera(CameraUpdateFactory.zoomTo(10), 2000, null);

// Construct a CameraPosition focusing on Mountain View and animate the camera to that position.
                CameraPosition cameraPosition = new CameraPosition.Builder()
                        .target(MOUNTAIN_VIEW)      // Sets the center of the map to Mountain View
                        .zoom(17)                   // Sets the zoom
                        .bearing(90)                // Sets the orientation of the camera to east
                        .tilt(30)                   // Sets the tilt of the camera to 30 degrees
                        .build();                   // Creates a CameraPosition from the builder
                mMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition));
            }
        }catch(Exception ex){
            Log.e(TAG,"Error in GoogleMap function",ex);
            Super_Library_URL SLU = new Super_Library_URL(TAG + "-" + ex.toString() , ((Activity) ctx));
        }
    }
}
