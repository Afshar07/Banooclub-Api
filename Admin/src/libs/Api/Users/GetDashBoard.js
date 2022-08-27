import WebServiceRequest from '../../Api/WebServiceRequest'

class GetDashBoard extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Dashboard/Index');
    }
}
export default GetDashBoard;
