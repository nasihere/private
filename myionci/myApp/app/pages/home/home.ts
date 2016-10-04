import {Page, NavController, NavParams} from 'ionic-angular';
import {LoginPage} from '../login/login';
import {RegisterPage} from '../register/register';
import {MoreShared} from '../moreshared/moreshared';

// For testing
import {OptionalPage} from '../optional/optional';
import {TabsPage} from '../tabs/tabs';

@Page({
  templateUrl: 'build/pages/home/home.html',
  providers: [MoreShared]
})
export class HomePage {
  // Declare the variables
  public pageinfo: any;
  public moreshared: any;
  constructor(private nav: NavController, params: NavParams, moreshared: MoreShared) {
    this.pageinfo = params.data;
    this.moreshared = moreshared;
  }

  // Testing
  openTest() {
    this.nav.push(TabsPage);
  }

  // Handle going to the how it works view
  openHowItWorks() {
  }

  // Handle going to the Login view
  openLogin() {
    this.nav.push(LoginPage);
  }

  // Handle going to the Register view
  openRegister() {
    this.nav.push(RegisterPage);
  }

  // Handle going to the TOS view
  openTOS() {
    this.moreshared.openTOS();
  }

  // Handle going to the Privacy view
  openPrivacy() {
    this.moreshared.openPrivacy();
  }

  // Handle going to the Rewards Terms view
  openRewardsTerms() {
    this.moreshared.openRewardsTerms();
  }

  // Handle going to the About Us view
  openAboutUs() {
    this.moreshared.openAboutUs();
  }

}