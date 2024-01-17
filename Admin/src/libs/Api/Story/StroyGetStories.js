import WebServiceRequest from '../../Api/WebServiceRequest'

class StoryGetStoryRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Story/GetStories')
    }
}
export default StoryGetStoryRequest;
