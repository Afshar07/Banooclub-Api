import WebServiceRequest from '../../Api/WebServiceRequest'

class PostDeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag('Post/Delete?id='+id)
    }
}
export default PostDeleteRequest;
