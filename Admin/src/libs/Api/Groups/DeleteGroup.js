import WebServiceRequest from '../../Api/WebServiceRequest'

class DeleteGroup extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag(`MessageGroup/Delete?id=${id}`)
    }
}
export default DeleteGroup;
