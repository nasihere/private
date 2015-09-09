package shifa.nasz.locally;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class UserCustomAdapterGrid extends BaseAdapter {
    private Context context;
    private final String[] mobileValues;
    private final String[] Record_OS;
    Super_Library_AppClass SLAc;
    String MyAccountName;
    String Category;

    public UserCustomAdapterGrid(Context context, String[] mobileValues, String[] Record_OS, String Category) {
        this.context = context;
        this.mobileValues = mobileValues;
        this.Record_OS = Record_OS;
        this.Category = Category;
        SLAc = new Super_Library_AppClass(context);
        MyAccountName = SLAc.GetPreferenceValue("ProfileName") + " - " + SLAc.GetPreferenceValue("ProfileMobile");

    }

    public View getView(int position, View convertView, ViewGroup parent) {

        LayoutInflater inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        View gridView;

        if (convertView == null) {

            gridView = new View(context);

            // get layout from mobile.xml
            gridView = inflater.inflate(R.layout.gridview_repertory_details, null);

            // set value into textview
            TextView textView = (TextView) gridView
                    .findViewById(R.id.grid_item_label);
            textView.setText(mobileValues[position]);


            TextView txtCounter = (TextView) gridView
                    .findViewById(R.id.grid_item_counter);
            try{
            int EntryRead = Integer.valueOf(SLAc.GetPreferenceValue("EntryRead"+Category+"|" + mobileValues[position]));
            Log.e("EntryRead", EntryRead + " ");
            if (Record_OS[position].equals("0")){
                txtCounter.setVisibility(View.GONE);
              }
            else if (EntryRead  != Integer.valueOf(Record_OS[position])){
                txtCounter.setVisibility(View.VISIBLE);
                txtCounter.setBackgroundColor(context.getResources().getColor(R.color.c_orange));
                int diff = Integer.valueOf(Record_OS[position]) - EntryRead;
                if (diff > 0)
                    txtCounter.setText(String.valueOf(diff));
            }
            else
            {
                txtCounter.setVisibility(View.GONE);
                txtCounter.setBackgroundColor(context.getResources().getColor(R.color.c_green));
                txtCounter.setText("");
            }}
            catch(Exception ex){

            }


            // set image based on selected text
            ImageView imageView = (ImageView) gridView
                    .findViewById(R.id.grid_item_image);

            String mobile = mobileValues[position];
            String Record = Record_OS[position];
            getImageIcon(mobile,imageView);

        } else {
            gridView = (View) convertView;
        }

        return gridView;
    }
    private void getImageIcon(String caption, ImageView img){
        String cap = caption.replaceAll(" ","_");
        if (caption.equalsIgnoreCase(MyAccountName))
            img.setBackgroundResource(R.drawable.ic_contact_icon);
        if (caption.indexOf("SMS To Mumbra") != -1)
            img.setImageResource(R.drawable.ic_sms);
        else if (cap.equalsIgnoreCase("apparels") || cap.equalsIgnoreCase("cloths"))

            img.setImageResource(R.drawable.ic_icon_merchandise_clothes);
        else if (cap.equalsIgnoreCase("atms"))

            img.setImageResource(R.drawable.ic__atm);
        else if (cap.equalsIgnoreCase("baby_school"))

            img.setImageResource(R.drawable.ic__baby_school);
        else if (cap.equalsIgnoreCase("bank"))

            img.setImageResource(R.drawable.ic__bank);

        else if (cap.equalsIgnoreCase("books"))

            img.setImageResource(R.drawable.ic__book);

        else if (cap.equalsIgnoreCase("church"))

            img.setImageResource(R.drawable.ic__church);

        else if (cap.equalsIgnoreCase("chemists"))

            img.setImageResource(R.drawable.ic__chemist);

        else if (cap.equalsIgnoreCase("Jobs"))

            img.setImageResource(R.drawable.ic_jobs);
        else if (cap.equalsIgnoreCase("company"))

            img.setImageResource(R.drawable.ic_ic_office_building);

        else if (cap.equalsIgnoreCase("classes"))

            img.setImageResource(R.drawable.ic__classes);
        else if (cap.equalsIgnoreCase("classes"))

            img.setImageResource(R.drawable.ic__classes);
        else if (cap.equalsIgnoreCase("buildings"))

            img.setImageResource(R.drawable.ic_companies);

        else if (cap.equalsIgnoreCase("computers"))

            img.setImageResource(R.drawable.ic__computer);

        else if (cap.equalsIgnoreCase("ngo"))

            img.setImageResource(R.drawable.ic__ngo);
        else if (cap.equalsIgnoreCase("gas"))

            img.setImageResource(R.drawable.ic__gas);

        else if (cap.equalsIgnoreCase("grocery"))

            img.setImageResource(R.drawable.ic_grocery);
        else if (cap.equalsIgnoreCase("gym"))

            img.setImageResource(R.drawable.ic__gym);

        else if (cap.equalsIgnoreCase("buy_or_sell"))

            img.setImageResource(R.drawable.ic_buy_sell_transparent);

        else if (cap.equalsIgnoreCase("mobile"))

            img.setImageResource(R.drawable.ic__mobile_shop);

        else if (cap.equalsIgnoreCase("motor_learning"))

            img.setImageResource(R.drawable.ic__motor_learning);

        else if (cap.equalsIgnoreCase("office"))

            img.setImageResource(R.drawable.ic__office);
        else if (cap.equalsIgnoreCase("News"))

            img.setImageResource(R.drawable.ic_ic__news);


        else if (cap.equalsIgnoreCase("pizza"))

            img.setImageResource(R.drawable.ic__pizza);

        else if (cap.equalsIgnoreCase("real_estate"))

            img.setImageResource(R.drawable.ic__real_estate);

        else if (cap.equalsIgnoreCase("restaurants"))

            img.setImageResource(R.drawable.ic_547b46f90507e7206e16bca4_restaurantmenuicon);

        else if (cap.equalsIgnoreCase("Police"))

            img.setImageResource(R.drawable.ic__security);

        else if (cap.equalsIgnoreCase("sports"))

            img.setImageResource(R.drawable.ic__sports);

        else if (cap.equalsIgnoreCase("station"))

            img.setImageResource(R.drawable.ic__station);
        else if (cap.equalsIgnoreCase("Train"))

            img.setImageResource(R.drawable.ic__station);


        else if (cap.equalsIgnoreCase("wedding") || cap.equalsIgnoreCase("hall") || cap.equalsIgnoreCase("marriage_hall"))

            img.setImageResource(R.drawable.ic__wedding);

        else if (cap.equalsIgnoreCase("xerox"))

            img.setImageResource(R.drawable.ic__xerox);


        else if (cap.equalsIgnoreCase("Temple"))

            img.setImageResource(R.drawable.ic__temple);
        else if (cap.equalsIgnoreCase("Tour_&_Travel"))

            img.setImageResource(R.drawable.ic__tour);
        else if (cap.equalsIgnoreCase("Masjid"))

            img.setImageResource(R.drawable.ic__masjid);

        else if (cap.equalsIgnoreCase("Software") || cap.equalsIgnoreCase("laptop"))

            img.setImageResource(R.drawable.ic__computer);


        else if (cap.equalsIgnoreCase("School_&_College"))

            img.setImageResource(R.drawable.ic__school);


    }

    @Override
    public int getCount() {
        return mobileValues.length;
    }

    @Override
    public Object getItem(int position) {
        return null;
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

}

