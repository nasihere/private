import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/tos/tos.html',
})
export class TosPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
