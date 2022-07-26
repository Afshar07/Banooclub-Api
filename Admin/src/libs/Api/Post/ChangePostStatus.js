import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangePostStatus extends WebServiceRequest {
    constructor (context) {
        super(context)

    }
    setParams(data){
        super.setTag(`Post/ChangePostStatusForAdmin?postId=${data.postId}&status=${data.status}`)
    }
}
export default ChangePostStatus;
