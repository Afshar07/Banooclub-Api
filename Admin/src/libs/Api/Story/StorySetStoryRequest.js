import WebServiceRequest from '../../Api/WebServiceRequest'

class StorySetStoryRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('Story/SetStory')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default StorySetStoryRequest;
