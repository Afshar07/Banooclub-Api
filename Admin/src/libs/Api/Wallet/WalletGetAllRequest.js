import WebServiceRequest from '../../Api/WebServiceRequest'

class WalletGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Wallet/GetAll?pageNumber='+data.pageNumber+'&count='+data.count+'&searchCommand='+data.search)
    }
}
export default WalletGetAllRequest;
