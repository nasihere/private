package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Filter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.shifa.kent.R;

import java.util.ArrayList;

public class ChatAdapter extends ArrayAdapter<ChatMain> {
    private static final int TAG_IMAGE_BTN_1 = 0;
    private static final int TAG_IMAGE_BTN_2 = 1;
    public ImageManager imageManager;
    Context context;
    int layoutResourceId;
    UserHolder holder = null;
    String SessionID = "";
    ArrayList<ChatMain> filterdata = new ArrayList<ChatMain>();
    ArrayList<ChatMain> data = new ArrayList<ChatMain>();

    public ChatAdapter(Context context, int layoutResourceId,
                       ArrayList<ChatMain> data, String SessionID) {
        super(context, layoutResourceId, data);
        this.layoutResourceId = layoutResourceId;

        this.context = context;
        this.filterdata = data;

        this.data = data;

        this.SessionID = SessionID;
        imageManager =
                new ImageManager(context.getApplicationContext());
    }

    public static boolean isNumeric(String str) {
        try {
            double d = Double.parseDouble(str);
        } catch (NumberFormatException nfe) {
            return false;
        }
        return true;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        View row = convertView;


        if (row == null) {

            LayoutInflater inflater = ((Activity) context).getLayoutInflater();
            row = inflater.inflate(layoutResourceId, parent, false);
            holder = new UserHolder();
            holder.textChatYellow = (TextView) row.findViewById(R.id.id_chat_shifamember_name);
            holder.textChatGreen = (TextView) row.findViewById(R.id.view_chatmsg);

            holder.imgChatGreen = (ImageView) row.findViewById(R.id.view_chaticoleft);
            holder.imgChatYellow = (ImageView) row.findViewById(R.id.id_chat_shifamember_logo);

            holder.linLoad_old_chatter = (LinearLayout) row.findViewById(R.id.linLoad_old_chatter);
            holder.imgChatUploadGreen = (ImageView) row.findViewById(R.id.imgToChat);
            holder.imgChatUploadYellow = (ImageView) row.findViewById(R.id.imgFromChat);

        } else {
            holder = (UserHolder) row.getTag();
        }

        row.setTag(holder);
        ChatMain user = filterdata.get(position);
        String sFrm = "";
        String sChat = "";

        try {
            sFrm = user.frm;
            sChat = user.chat;
        } catch (Exception e) {
            sFrm = "---";

        }

        if (position == 0) {
            if (user.chat_type.equals("chat")) {
                holder.linLoad_old_chatter.setVisibility(View.VISIBLE);
            } else {
                holder.linLoad_old_chatter.setVisibility(View.GONE);

            }
        } else {

            holder.linLoad_old_chatter.setVisibility(View.GONE);
        }

        if (sFrm != null && sFrm.equals(SessionID)) {
            holder.textChatYellow.setVisibility(View.GONE);
            holder.imgChatYellow.setVisibility(View.GONE);
            holder.imgChatUploadGreen.setVisibility(View.GONE);


            if (user.picture == null || user.picture.equals("")) {
                holder.imgChatUploadYellow.setVisibility(View.GONE);

            } else {
                imageManager.displayImage(user.picture, holder.imgChatUploadYellow, R.drawable.ic_launcher);
                holder.imgChatUploadYellow.setVisibility(View.VISIBLE);
                holder.imgChatGreen.setVisibility(View.VISIBLE);
            }

            final String PictureURI = user.picture;
            holder.imgChatUploadYellow.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {

                    Intent intent = new Intent(context, ImageViewFullScreen.class);
                    intent.putExtra("imageUrl", PictureURI);
                    context.startActivity(intent);


                }
            });

            if (sChat == null || sChat.equals("")) {
                holder.textChatGreen.setVisibility(View.GONE);
            } else {
                String ascii = "\u2713";
                if (user.id_web == null) {
                    sChat += ascii;
                } else {
                    sChat += ascii + ascii;
                }
                holder.textChatGreen.setText(Html.fromHtml(sChat), TextView.BufferType.SPANNABLE);
                holder.textChatGreen.setVisibility(View.VISIBLE);
                holder.imgChatGreen.setVisibility(View.VISIBLE);
            }

            if (isNumeric(sFrm) == true)
                imageManager.displayImage(sFrm, holder.imgChatGreen, R.drawable.ic_launcher);
            else
                holder.imgChatGreen.setImageResource(R.drawable.ic_launcher);

            final String PictureURIsFrm = sFrm;
            holder.imgChatGreen.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {

                    Intent intent = new Intent(context, ImageViewFullScreen.class);
                    intent.putExtra("imageUrl", PictureURIsFrm);
                    context.startActivity(intent);


                }
            });


        } else {

            holder.imgChatGreen.setVisibility(View.GONE);
            holder.imgChatUploadYellow.setVisibility(View.GONE);
            holder.textChatGreen.setVisibility(View.GONE);


            if (user.picture == null || user.picture.equals("")) {
                holder.imgChatUploadGreen.setVisibility(View.GONE);
            } else {
                imageManager.displayImage(user.picture, holder.imgChatUploadGreen, R.drawable.ic_launcher);
                holder.imgChatUploadGreen.setVisibility(View.VISIBLE);
                holder.imgChatYellow.setVisibility(View.VISIBLE);

            }

            final String PictureURI = user.picture;
            holder.imgChatUploadGreen.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {

                    Intent intent = new Intent(context, ImageViewFullScreen.class);
                    intent.putExtra("imageUrl", PictureURI);
                    context.startActivity(intent);


                }
            });


            if (sChat == null || sChat.equals("")) {
                holder.textChatYellow.setVisibility(View.GONE);
            } else {
                String ascii = "\u2713";
                if (user.id_web == null) {
                    sChat += ascii;
                } else {
                    sChat += ascii + ascii;
                }
                holder.textChatYellow.setText(Html.fromHtml(sChat), TextView.BufferType.SPANNABLE);
                holder.textChatYellow.setVisibility(View.VISIBLE);
                holder.imgChatYellow.setVisibility(View.VISIBLE);
            }


            if (isNumeric(sFrm) == true)
                imageManager.displayImage(sFrm, holder.imgChatYellow, R.drawable.ic_launcher);
            else
                holder.imgChatYellow.setImageResource(R.drawable.ic_launcher);


            final String PictureURIsFrm = sFrm;
            holder.imgChatYellow.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {

                    Intent intent = new Intent(context, activity_chat_settings_friends.class);
                    intent.putExtra("imageUrl", PictureURIsFrm);
                    intent.putExtra(".XXXXXXX_to", PictureURIsFrm);

                    context.startActivity(intent);


                }
            });
        }


        holder.imgChatYellow.setTag(user);


        return row;

    }

    @Override
    public Filter getFilter() {
        return new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence constraint) {
                FilterResults oReturn = new FilterResults();
                ArrayList<ChatMain> results = new ArrayList<ChatMain>();

                ArrayList<ChatMain> orig = new ArrayList<ChatMain>();
                //log.e("orig","empty");

                //log.e("orig","true");
                orig = data;
                //log.e("orig","false");

                //log.e("orig",String.valueOf(orig.size()));
                if (constraint != null) {
                    if (orig != null && orig.size() > 0) {
                        for (final ChatMain g : orig) {
                            String[] sWords = constraint.toString().split(" ");
                            int iMatched = 0;
                            for (String sWrd : sWords) {

                                //log.e("sWrd ", sWrd);
                                if (g.chat.toLowerCase()
                                        .contains(sWrd.toLowerCase().toString())) {
                                    iMatched++;


                                } else if (g.chatter.toLowerCase()
                                        .contains(sWrd.toLowerCase().toString())) {
                                    iMatched++;


                                }


                            }
                            //log.e("sWrd.length()", String.valueOf(sWords.length));
                            //log.e("iMatched ", String.valueOf(iMatched));
                            if (iMatched == sWords.length) {

                                results.add(g);

                            }

                        }
                    }
                    oReturn.count = results.size();
                    oReturn.values = results;
                }
                return oReturn;
            }

            @SuppressWarnings("unchecked")
            @Override
            protected void publishResults(CharSequence constraint,
                                          FilterResults results) {

                ArrayList<ChatMain> items;
                //log.e("orig","notifyDataSetChanged");

                items = (ArrayList<ChatMain>) results.values;

                //log.e("orig","notifyDataS items etChanged");
                notifyDataSetChanged(items);
            }
        };
    }

    public void notifyDataSetChanged(ArrayList<ChatMain> performfilter) {
        super.notifyDataSetChanged();
        //log.e("runnable","action");
        this.filterdata = performfilter;
        //log.e("runnable","action end");
        // notifyChanged = true;
    }

    @Override
    public int getCount() {
        return filterdata.size();
    }

    static class UserHolder {
        TextView textChatYellow;
        TextView textChatGreen;
        ImageView imgChatGreen;

        ImageView imgChatYellow;
        TextView txtLoadMsg;
        ImageView imgChatUploadYellow;
        ImageView imgChatUploadGreen;
        LinearLayout linLoad_old_chatter;
    }


}
