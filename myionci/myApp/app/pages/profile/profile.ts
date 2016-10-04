import {Page, NavController, NavParams} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/profile/profile.html',
})
export class ProfilePage {
  constructor(private nav: NavController) {
    console.log("profile page");
  }
}