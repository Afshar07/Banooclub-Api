import WebServiceRequest from '../../Api/WebServiceRequest'
class DeleteRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setId(id){
        super.setTag(`Roomate/Delete?id=${id}`)
    }
}
export default DeleteRequest;

