import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/optional/optional.html',
  providers: [MoreShared]
})
export class OptionalPage {
  // Definitions
  public optionalForm: any;         // Optional form info.
  public moreshared: any;           // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared) {
    this.optionalForm = fb.group({
      countrycode: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required]
    });
    this.moreshared = moreshared;
  }

  // Handle done
  handleOptionalDone() {
    console.log('handleOptionalDone');
  }

  // Handle skip
  handleSkipOptional() {
    console.log('handleSkipOptional');
  }

  // Handle assigning the avatar
  handleSetAvatar() {
    console.log('handleSetAvatar');
  }

  // Handle assigning the cover
  handleSetCover() {
    console.log('handleSetCover');
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