WorldBank solution:
- Models should have simple and single responsibility with minimum logic for what it does.
- Complicated logic (transfer, deposit, etc.) should be handled by services
- Using Guid instead of using int for account number or customer id
- Account Number property is not presented in Account because, in this scenario, it acts just like id
- Each transaction will be kept by both from and to account for better auditing when needed
- Each customer will have one Cash account, multiple Banking account