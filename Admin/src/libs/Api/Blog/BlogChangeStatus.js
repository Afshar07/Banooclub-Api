import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangeBlogStatus extends WebServiceRequest {
    constructor (context) {
        super(context)

    }
    setParams(data){
        super.setTag(`Blog/ChangeBlogStatus?status=${data.status}`)
    }
    setParamsWithBody(data){
        super.setRequestParamDataObj(data)
    }
}
export default ChangeBlogStatus;
