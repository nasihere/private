import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Pages to use
import {OptionalPage} from '../optional/optional';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/required/required.html',
  providers: [MoreShared]
})
export class RequiredPage {
  // Definitions
  public requiredForm: any;             // Required form info.
  public moreshared: any;               // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared) {
    this.requiredForm = fb.group({
      name: ['', Validators.required],
      username: ['', Validators.required],
      birthday: ['', Validators.required],
      gender: ['', Validators.required],
      email: ['', Validators.required],
      mobile: ['', Validators.required]
    });
    this.moreshared = moreshared;
  }

  // Handle next
  handleRequiredNext() {
    this.nav.push(OptionalPage);
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