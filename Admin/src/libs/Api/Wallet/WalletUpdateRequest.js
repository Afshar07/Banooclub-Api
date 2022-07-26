import WebServiceRequest from '../../Api/WebServiceRequest'

class WalletUpdateRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('Wallet/updateWallet?credit='+data.credit+'&userId='+data.userId+'')
    }
}
export default WalletUpdateRequest;
