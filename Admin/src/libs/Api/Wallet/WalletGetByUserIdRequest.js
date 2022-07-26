import WebServiceRequest from '../../Api/WebServiceRequest'

class WalletGetByUserIdRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setUserId(id){
        super.setTag('Wallet/GetByUserId?userId='+id)
    }
}
export default WalletGetByUserIdRequest;
