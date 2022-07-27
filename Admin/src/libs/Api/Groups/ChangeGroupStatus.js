import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangeGroupStatus extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag(`MessageGroup/ChangeGroupStatus?groupId=${id}`)
    }
}
export default ChangeGroupStatus;
