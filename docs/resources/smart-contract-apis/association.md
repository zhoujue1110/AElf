# Association Contract

## **CreateOrganization**

```Protobuf
rpc CreateOrganization (CreateOrganizationInput) returns (aelf.Address) { }

message CreateOrganizationInput {
    OrganizationMemberList organization_member_list = 1;
    acs3.ProposalReleaseThreshold proposal_release_threshold = 2;
    acs3.ProposerWhiteList proposer_white_list = 3;
}
message OrganizationMemberList {
    repeated aelf.Address organization_members = 1;
}

message OrganizationCreated{
    option (aelf.is_event) = true;
    aelf.Address organization_address = 1;
}
```

Creates an organization and returns its address.

**CreateOrganizationInput**:
- **organizer member list**: 
  - **organization_members**: initial organization members.
- **ProposalReleaseThreshold**:
  - **minimal approval threshold**: the value for the minimum approval threshold.
  - **maximal rejection threshold**: the value for the maximal rejection threshold.
  - **maximal abstention threshold**: the value for the maximal abstention threshold.
  - **minimal vote threshold**: the value for the minimal vote threshold.
- **ProposerWhiteList**:
  - **proposers**: proposer whitelist.

After a successful execution, an **OrganizationCreated** event log can be found in the transaction result.

**OrganizationCreated**:
- **organization address**: the address of newly created organization

## **CreateOrganizationBySystemContract**

```Protobuf
rpc CreateOrganizationBySystemContract(CreateOrganizationBySystemContractInput) returns (aelf.Address){}

message CreateOrganizationBySystemContractInput {
    CreateOrganizationInput organization_creation_input = 1;
    string organization_address_feedback_method = 2;
}

message CreateOrganizationInput{
    OrganizationMemberList organization_member_list = 1;
    acs3.ProposalReleaseThreshold proposal_release_threshold = 2;
    acs3.ProposerWhiteList proposer_white_list = 3;
}

message OrganizationMemberList {
    repeated aelf.Address organization_members = 1;
}

message OrganizationCreated{
    option (aelf.is_event) = true;
    aelf.Address organization_address = 1;
}
```
Creates an organization by system contract and returns its address.

**CreateOrganizationBySystemContractInput**:
- **CreateOrganizationInput**:
  - **OrganizationMemberList**: 
    - **organization_members**: initial organization members.
  - **ProposalReleaseThreshold**:
    - **minimal approval threshold**: the value for the minimum approval threshold.
    - **maximal rejection threshold**: the value for the maximal rejection threshold.
    - **maximal abstention threshold**: the value for the maximal abstention threshold.
    - **minimal vote threshold**: the value for the minimal vote threshold.
  - **ProposerWhiteList**:
    - **proposers**: proposer whitelist.
- **organization address feedback method**: organization address callback method which replies the organization address to caller contract.

After a successful execution, an **OrganizationCreated** event log can be found in the transaction result.

**OrganizationCreated**:
- **organization address**: the address of newly created organization

## **ChangeOrganizationMember**

```Protobuf
rpc ChangeOrganizationMember(OrganizationMemberList) returns (google.protobuf.Empty) { }

message OrganizationMemberList {
    repeated aelf.Address organization_members = 1;
}

message OrganizationMemberChanged {
    option (aelf.is_event) = true;
    aelf.Address organization_address = 1;
    OrganizationMemberList organization_member_list = 2;
}
```

Changes the members of the organization. Note that this will override the current list.

- **OrganizationMemberList**:
  - **organization_members**: the new members.

After a successful execution, an **OrganizationMemberChanged** event log can be found in the transaction result.

**OrganizationMemberChanged**:
- **organization_address**: the organization address.
- **organization_member_list**: the new member list.

# View methods

## **GetOrganization**

```Protobuf
rpc GetOrganization (aelf.Address) returns (Organization) { }

message Organization {
    OrganizationMemberList organization_member_list = 1;
    acs3.ProposalReleaseThreshold proposal_release_threshold = 2;
    acs3.ProposerWhiteList proposer_white_list = 3;
    aelf.Address organization_address = 4;
    aelf.Hash organization_hash = 5;
}
```

Returns the organization with the specified address.

**Organization**:
- **member list**: original members of this organization.
- **ProposalReleaseThreshold**:
  - **minimal approval threshold**: the value for the minimum approval threshold.
  - **maximal rejection threshold**: the value for the maximal rejection threshold.
  - **maximal abstention threshold**: the value for the maximal abstention threshold.
  - **minimal vote threshold**: the value for the minimal vote threshold.
- **ProposerWhiteList**:
  - **proposers**: proposer list.
- **address**: the organizations address.
- **hash**: the organizations ID.

## **CalculateOrganizationAddress**

```Protobuf
rpc CalculateOrganizationAddress(CreateOrganizationInput) returns (aelf.Address){}

message CreateOrganizationInput {
    string token_symbol = 1;
    acs3.ProposalReleaseThreshold proposal_release_threshold = 2;
    acs3.ProposerWhiteList proposer_white_list = 3;
}
```

**CreateOrganizationInput**:
- **organizer member list**: 
  - **organization_members**: initial organization members.
- **ProposalReleaseThreshold**:
  - **minimal approval threshold**: the value for the minimum approval threshold.
  - **maximal rejection threshold**: the value for the maximal rejection threshold.
  - **maximal abstention threshold**: the value for the maximal abstention threshold.
  - **minimal vote threshold**: the value for the minimal vote threshold.
- **ProposerWhiteList**:
  - **proposers**: proposer whitelist.

Calculate with input and return the organization address.

# **ACS3 specific methods**

## **CreateProposal**

```Protobuf
rpc CreateProposal (CreateProposalInput) returns (aelf.Hash) { }

message CreateProposalInput {
    string contract_method_name = 1;
    aelf.Address to_address = 2;
    bytes params = 3;
    google.protobuf.Timestamp expired_time = 4;
    aelf.Address organization_address = 5;
    string proposal_description_url = 6,
    aelf.Hash token = 7;
}

message ProposalCreated{
    option (aelf.is_event) = true;
    aelf.Hash proposal_id = 1;
}
```

This method creates a proposal for which organization members can vote. When the proposal is released, a transaction will be sent to the specified contract.

**returns:** the ID of the newly created proposal.

**CreateProposalInput**:
- **contract method name**: the name of the method to call after release.
- **to address**: the address of the contract to call after release.
- **expiration**: the timestamp at which this proposal will expire.
- **organization address**: the address of the organization.
- **proposal_description_url**: the url is used for proposal describing.
- **token**: the token is for proposal id generation and with this token, proposal id can be calculated before proposing. 

After a successful execution, a **ProposalCreated** event log can be found in the transaction result.

**ProposalCreated**:
- **proposal_id**: the id of the created proposal.

## **Approve**

```Protobuf
    rpc Approve (aelf.Hash) returns (google.protobuf.Empty) {}
    
    message ReceiptCreated {
        option (aelf.is_event) = true;
        aelf.Hash proposal_id = 1;
        aelf.Address address = 2;
        string receipt_type = 3;
        google.protobuf.Timestamp time = 4;
    }
```
This method is called to approve the specified proposal.

**Hash**: the id of the proposal.

After a successful execution, a **ReceiptCreated** event log can be found in the transaction result.

**ReceiptCreated**:
- **proposal id**: id of proposal to reject.
- **address**: send address who votes for approval.
- **receipt type**: Approve.
- **time**: timestamp of this method call.

## **Reject**

```Protobuf
    rpc Reject(aelf.Hash) returns (google.protobuf.Empty) { }
    
    message ReceiptCreated {
        option (aelf.is_event) = true;
        aelf.Hash proposal_id = 1;
        aelf.Address address = 2;
        string receipt_type = 3;
        google.protobuf.Timestamp time = 4;
    }
```

This method is called to reject the specified proposal.

**Hash**: the id of the proposal.

After a successful execution, a **ReceiptCreated** event log can be found in the transaction result.

**ReceiptCreated**:
- **proposal id**: id of proposal to reject.
- **address**: send address who votes for rejection.
- **receipt type**: Reject.
- **time**: timestamp of this method call.

## **Abstain**

```Protobuf
    rpc Abstain(aelf.Hash) returns (google.protobuf.Empty) { }

    message ReceiptCreated {
        option (aelf.is_event) = true;
        aelf.Hash proposal_id = 1;
        aelf.Address address = 2;
        string receipt_type = 3;
        google.protobuf.Timestamp time = 4;
    }
```

This method is called to abstain from the specified proposal.

**Hash**: the id of the proposal.

After a successful execution, a **ReceiptCreated** event log can be found in the transaction result.

**ReceiptCreated**:
- **proposal id**: id of proposal to abstain.
- **address**: send address who votes for abstention.
- **receipt type**: Abstain.
- **time**: timestamp of this method call.

## **Release**

```Protobuf
    rpc Release(aelf.Hash) returns (google.protobuf.Empty) { }
```

This method is called to release the specified proposal.

**Hash**: the id of the proposal.

## **ChangeOrganizationThreshold**

```Protobuf
rpc ChangeOrganizationThreshold(ProposalReleaseThreshold) returns (google.protobuf.Empty) { }

message ProposalReleaseThreshold {
    int64 minimal_approval_threshold = 1;
    int64 maximal_rejection_threshold = 2;
    int64 maximal_abstention_threshold = 3;
    int64 minimal_vote_threshold = 4;
}

message OrganizationThresholdChanged{
    option (aelf.is_event) = true;
    aelf.Address organization_address = 1;
    ProposalReleaseThreshold proposer_release_threshold = 2;
}
```

This method changes the thresholds associated with proposals. All fields will be overwritten by the input value and this will affect all current proposals of the organization. Note: only the organization can execute this through a proposal.

**ProposalReleaseThreshold**:
- **minimal approval threshold**: the new value for the minimum approval threshold.
- **maximal rejection threshold**: the new value for the maximal rejection threshold.
- **maximal abstention threshold**: the new value for the maximal abstention threshold.
- **minimal vote threshold**: the new value for the minimal vote threshold.

After a successful execution, an **OrganizationThresholdChanged** event log can be found in the transaction result.

**OrganizationThresholdChanged**:
- **organization_address**: the organization address.
- **proposer_release_threshold**: the new release threshold.

## **ChangeOrganizationProposerWhiteList**

```Protobuf
rpc ChangeOrganizationProposerWhiteList(ProposerWhiteList) returns (google.protobuf.Empty) { }

message ProposerWhiteList {
    repeated aelf.Address proposers = 1;
}

message OrganizationWhiteListChanged{
    option (aelf.is_event) = true;
    aelf.Address organization_address = 1;
    ProposerWhiteList proposer_white_list = 2;
}
```

This method overrides the list of whitelisted proposers.

**ProposerWhiteList**:
- **proposers**: the new value for the list.

After a successful execution, a **OrganizationWhiteListChanged** event log can be found in the transaction result.

**OrganizationWhiteListChanged**:
- **organization_address**: the organization address.
- **proposer_white_list**: the new proposer whitelist.

## **CreateProposalBySystemContract**

```Protobuf
rpc CreateProposalBySystemContract(CreateProposalBySystemContractInput) returns (aelf.Hash) { }

message CreateProposalBySystemContractInput {
    acs3.CreateProposalInput proposal_input = 1;
    aelf.Address origin_proposer = 2;
}

message ProposalCreated{
    option (aelf.is_event) = true;
    aelf.Hash proposal_id = 1;
}
```

Used by system contracts to create proposals.

**CreateProposalBySystemContractInput**:
- **CreateProposalInput**: 
  - **contract method name**: the name of the method to call after release.
  - **to address**: the address of the contract to call after release.
  - **expiration**: the date at which this proposal will expire.
  - **organization address**: the address of the organization.
  - **proposal_description_url**: the url is used for proposal describing.
  - **token**: the token is for proposal id generation and proposal id can be calculated before proposing. 
- **origin proposer**: the actor that trigger the call.

After a successful execution, a **OrganizationWhiteListChanged** event log can be found in the transaction result.

**ProposalCreated**:
- **proposal_id**: the id of the created proposal.

## **ClearProposal**

```Protobuf
rpc ClearProposal(aelf.Hash) returns (google.protobuf.Empty) { }
```

Removes the specified proposal.

## **ValidateOrganizationExist**

```Protobuf
rpc ValidateOrganizationExist(aelf.Address) returns (google.protobuf.BoolValue) { }
```

Checks the existence of an organization.

# View methods

## **GetProposal**

```Protobuf
rpc GetProposal(aelf.Hash) returns (ProposalOutput) { }

message ProposalOutput {
    aelf.Hash proposal_id = 1;
    string contract_method_name = 2;
    aelf.Address to_address = 3;
    bytes params = 4;
    google.protobuf.Timestamp expired_time = 5;
    aelf.Address organization_address = 6;
    aelf.Address proposer = 7;
    bool to_be_released = 8;
    int64 approval_count = 9;
    int64 rejection_count = 10;
    int64 abstention_count = 11;
}
```

Get the proposal with the given ID.

**ProposalOutput**:
- **proposal id**: ID of the proposal.
- **method name**: the method that this proposal will call when being released.
- **to address**: the address of the target contract.
- **params**: the parameters of the release transaction.
- **expiration**: the date at which this proposal will expire.
- **organization address**: address of this proposals organization.
- **proposer**: address of the proposer of this proposal.
- **to be release**: indicates if this proposal is releasable.
- **approval count**: approval count for this proposal
- **rejection count**: rejection count for this proposal
- **abstention count**: abstention count for this proposal


## **ValidateProposerInWhiteList**

```Protobuf
rpc ValidateProposerInWhiteList(ValidateProposerInWhiteListInput) returns (google.protobuf.BoolValue) { }

message ValidateProposerInWhiteListInput {
    aelf.Address proposer = 1;
    aelf.Address organization_address = 2;
}
```

Checks if the proposer is whitelisted.

**ValidateProposerInWhiteListInput**:
- **proposer**: the address to search/check.
- **organization address**: address of the organization.

