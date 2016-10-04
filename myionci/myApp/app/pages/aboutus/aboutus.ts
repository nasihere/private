import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/aboutus/aboutus.html',
})
export class AboutUsPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
