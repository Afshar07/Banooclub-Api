import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangePostStatus extends WebServiceRequest {
    constructor (context) {
        super(context)

    }
    setParams(data){
        super.setTag(`Post/ChangePostStatusForAdmin?status=${data.status}`)
    }
    setParamsWithBody(data){
        super.setRequestParamDataObj(data)
    }
}
export default ChangePostStatus;
