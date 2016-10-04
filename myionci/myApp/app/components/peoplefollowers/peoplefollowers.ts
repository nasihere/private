import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'peoplefollowers',
  templateUrl: 'build/components/peoplefollowers/peoplefollowers.html',
  inputs: ['feedinfo'],
  directives: [IONIC_DIRECTIVES]
})
export class peopleFollowers {
  constructor() {
  }

  // Handle the Challenge person
  handleChallenge(feedinfo) {
    console.log("handleChallenge");
  }

  // Handle following the person
  handleFollow(feedinfo) {
    console.log("handleFollow");
    feedinfo.following = true;
  }

  // Handle unfollowing the person
  handleUnfollow(feedinfo) {
    console.log("handleUnfollow");
    feedinfo.following = false;
  }
}
