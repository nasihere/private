package com.shifa.kent.chatsdk;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.text.Html;
import android.util.Log;
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

public class ChatAdapterView extends ArrayAdapter<ChatMain> {
    private static final int TAG_IMAGE_BTN_1 = 0;
    private static final int TAG_IMAGE_BTN_2 = 1;
    public ImageManager imageManager;
    Context context;
    int layoutResourceId;
    UserHolder holder = null;
    String SessionID = "";
    ArrayList<ChatMain> filterdata = new ArrayList<ChatMain>();
    ArrayList<ChatMain> data = new ArrayList<ChatMain>();
    String ChatTitle = "";

    public ChatAdapterView(Context context, int layoutResourceId,
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
        if (str == null) return false;
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
            holder.id_chat_pvtmsg_menu_short_msg = (TextView) row.findViewById(R.id.id_chat_pvtmsg_menu_short_msg);
            holder.id_chat_pvtmsg_menu_datetime = (TextView) row.findViewById(R.id.id_chat_shifamember_datetime);
            holder.linLoadMore = (LinearLayout) row.findViewById(R.id.id_lin_LoadMoreList);
            holder.id_chat_shifamember_logo = (ImageView) row.findViewById(R.id.id_chat_shifamember_logo);
            holder.id_chat_pvtmsg_menu_name = (TextView) row.findViewById(R.id.id_chat_shifamember_name);
            holder.img_list_type = (ImageView) row.findViewById(R.id.img_list_type);
            holder.tvChatTitle = (TextView) row.findViewById(R.id.tvChatTitle);

        } else {
            holder = (UserHolder) row.getTag();
        }

        row.setTag(holder);
        ChatMain user = filterdata.get(position);
        String sFrm = "";
        String sChat = "";
        try {
            //Log.e("Chatadapter", SessionID);
            sFrm = user.getFrm().toString();
            sChat = user.chat.toString();
            //Log.e("Chatadapter", sFrm);
        } catch (Exception e) {

        }

        holder.tvChatTitle.setVisibility(View.GONE);
        if (null != user.chat_type) {
            if (!user.chat_type.equals(ChatTitle) || position == 0) {
                ChatTitle = user.chat_type;
                holder.tvChatTitle.setVisibility(View.VISIBLE);
                if (user.chat_type.equals("chat")) {
                    holder.tvChatTitle.setText("Group Chat");
                } else if (user.chat_type.equals("disc")) {
                    holder.tvChatTitle.setText("Discussion Post & Reply");
                } else if (user.chat_type.equals("msg")) {
                    holder.tvChatTitle.setText("Private Messages");
                } else if (user.chat_type.equals("Friends")) {
                    if (user.expert == 1) {

                        holder.tvChatTitle.setText("Experts");
                    } else {
                        holder.tvChatTitle.setText("Recent Friends");
                    }
                } else {
                    holder.tvChatTitle.setVisibility(View.VISIBLE);
                }


            }

        }

        if (position == getCount() - 1) {
            if (user.chat_type.equals("Friends")) {
                holder.linLoadMore.setVisibility(View.GONE);
            } else {
                holder.linLoadMore.setVisibility(View.VISIBLE);

            }
        } else {
            holder.linLoadMore.setVisibility(View.GONE);
        }

        final String PictureURIsFrm = user.frm;
        holder.id_chat_shifamember_logo.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {

                Intent intent = new Intent(context, activity_chat_settings_friends.class);
                intent.putExtra("imageUrl", PictureURIsFrm);
                intent.putExtra(".XXXXXXX_to", PictureURIsFrm);

                context.startActivity(intent);


            }
        });

        if (isNumeric(user.frm) == true)
            imageManager.displayImage(user.frm, holder.id_chat_shifamember_logo, R.drawable.ic_launcher);
        else
            holder.id_chat_shifamember_logo.setImageResource(R.drawable.ic_launcher);


              /*  String ascii ="\u2713";
                if (user.iRead == 1)
                {
               //     sChat += ascii + ascii;
                }*/
        if (user.iRead == 1) {
            holder.id_chat_pvtmsg_menu_short_msg.setText(Html.fromHtml("<b>" + sChat + "</b>"), TextView.BufferType.SPANNABLE);
        } else {
            holder.id_chat_pvtmsg_menu_short_msg.setText(Html.fromHtml(sChat), TextView.BufferType.SPANNABLE);
        }
        holder.id_chat_pvtmsg_menu_name.setText(user.chatter);
        if (user.datetime != null) {
            holder.id_chat_pvtmsg_menu_datetime.setText(Html.fromHtml(user.datetime), TextView.BufferType.SPANNABLE);
        } else {
            holder.id_chat_pvtmsg_menu_datetime.setText("");
        }

        holder.id_chat_shifamember_logo.setTag(user);


        if (user.chat_type.equals("msg")) {
            holder.img_list_type.setImageResource(R.drawable.ic_content_markunread);
        } else if (user.chat_type.equals("disc")) {
            holder.img_list_type.setImageResource(R.drawable.ic_action_account_child);
        } else if (user.chat_type.equals("chat")) {
            holder.img_list_type.setImageResource(R.drawable.ic_communication_forum);
        } else if (user.chat_type.equals("Friends")) {
            if (user.expert == 1) {
                holder.img_list_type.setImageResource(R.drawable.ic_action_action_grade);
            } else {
                holder.img_list_type.setImageResource(R.drawable.ic_action_image_timer_auto_white);
            }
        }

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
                Log.e("orig", "empty");

                Log.e("orig", "true");
                orig = data;
                Log.e("orig", "false");

                Log.e("orig", String.valueOf(orig.size()));
                if (constraint != null) {
                    if (orig != null && orig.size() > 0) {
                        for (final ChatMain g : orig) {
                            String[] sWords = constraint.toString().split(" ");
                            int iMatched = 0;
                            for (String sWrd : sWords) {

                                Log.e("sWrd ", sWrd);
                                if (g.chat.toLowerCase()
                                        .contains(sWrd.toLowerCase().toString())) {
                                    iMatched++;


                                } else if (g.chatter.toLowerCase()
                                        .contains(sWrd.toLowerCase().toString())) {
                                    iMatched++;


                                }


                            }
                            Log.e("sWrd.length()", String.valueOf(sWords.length));
                            Log.e("iMatched ", String.valueOf(iMatched));
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
                Log.e("orig", "notifyDataSetChanged");

                items = (ArrayList<ChatMain>) results.values;

                Log.e("orig", "notifyDataS items etChanged");
                notifyDataSetChanged(items);
            }
        };
    }

    public void notifyDataSetChanged(ArrayList<ChatMain> performfilter) {
        super.notifyDataSetChanged();
        Log.e("runnable", "action");
        this.filterdata = performfilter;
        Log.e("runnable", "action end");
        // notifyChanged = true;
    }

    @Override
    public int getCount() {
        return filterdata.size();
    }

    static class UserHolder {
        TextView id_chat_pvtmsg_menu_short_msg;
        TextView id_chat_pvtmsg_menu_datetime;
        ImageView id_chat_shifamember_logo;
        LinearLayout linLoadMore;
        TextView id_chat_pvtmsg_menu_name;
        ImageView img_list_type;
        TextView tvChatTitle;
    }
}
