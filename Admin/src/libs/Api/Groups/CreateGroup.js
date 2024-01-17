import WebServiceRequest from '../../Api/WebServiceRequest'

class CreateGroup extends WebServiceRequest {
    constructor (context) {
        super(context)
        super.setTag('MessageGroup/Create')
    }
    setParams(data){
        super.setRequestParamDataObj(data)
    }
}
export default CreateGroup;
