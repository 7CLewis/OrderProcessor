INSERT INTO Products (
    Name,
    Description,
    UnitOfMeasure,
    Quantity,
    PricePerUnit,
    CreatedDateTime
)
VALUES
('Premium Copy Paper',
    'A ream of high-quality 20 lb copy paper. Acid-free and jam-resistant.',
    'Ream',
    1,
    '38.99',
    GETUTCDATE()),
('Bulk Premium Copy Paper',
    'A box of high-quality 20 lb copy paper. Acid-free and jam-resistant.',
    'Ream',
    6,
    '38.99',
    GETUTCDATE()),

('Bulk Recycled Copy Paper',
    'Eco-friendly reams made from 30% post-consumer materials.',
    'Ream',
    10,
    '9.49',
    GETUTCDATE()),

('Cardstock Paper',
    'Heavy-weight cardstock for presentations and signage.',
    'Each',
    1,
    '1.99',
    GETUTCDATE()),

('Cardstock Paper 2-Pack',
    'Heavy-weight cardstock for presentations and signage.',
    'Pack',
    2,
    '12.99',
    GETUTCDATE()),

('Legal Size Copy Paper',
    'Legal-size copy paper for contracts and legal documents.',
    'Ream',
    1,
    '10.49',
    GETUTCDATE()),

('Colored Copy Paper',
    'Assorted-color copy paper for flyers, notices, and office communication.',
    'Ream',
    1,
    '11.99',
    GETUTCDATE()),

('Manila File Folders',
    'Box of 200 durable manila file folders for office organization.',
    'Box',
    1,
    '27.49',
    GETUTCDATE()),

('Hanging File Folders',
    '80-count box of reinforced hanging file folders with tabs.',
    'Box',
    1,
    '42.49',
    GETUTCDATE()),

('Yellow Legal Pads',
    'Classic yellow legal pads for meetings and notes.',
    'Pack',
    24,
    '41.49',
    GETUTCDATE()),

('Sticky Notes (3x3)',
    'Yellow sticky notes for reminders and quick notes.',
    'Pack',
    12,
    '14.99',
    GETUTCDATE()),

('Shipping Boxes - Small',
    'Durable corrugated shipping boxes suitable for lightweight office items.',
    'Each',
    1,
    '1.25',
    GETUTCDATE()),

('Shipping Boxes - Medium',
    'Medium-sized corrugated boxes for general shipping needs.',
    'Each',
    1,
    '2.49',
    GETUTCDATE()),

('Shipping Boxes - Large',
    'Large corrugated boxes for bulky items and equipment.',
    'Each',
    1,
    '3.99',
    GETUTCDATE());

INSERT INTO TransportCompanies (
    Name,
    ContactPhone,
    HeadquartersAddress_Line1,
    HeadquartersAddress_Line2,
    HeadquartersAddress_City,
    HeadquartersAddress_StateOrProvince,
    HeadquartersAddress_PostalCode,
    HeadquartersAddress_Country
)
VALUES
    ('Scranton Freight Co',
        '570-555-1000',
        '1725 Slough Avenue',
        NULL,
        'Scranton',
        'PA',
        '18505',
        'USA'),

    ('Pennsylvania Express Logistics',
        '717-555-2200',
        '400 Keystone Parkway',
        'Suite 200',
        'Harrisburg',
        'PA',
        '17101',
        'USA'),

    ('Northeast Regional Shipping',
        '201-555-3300',
        '88 Industrial Way',
        NULL,
        'Newark',
        'NJ',
        '07102',
        'USA');

INSERT INTO Orders (
    CreatedUtc,
    SenderAddress_Line1,
    SenderAddress_Line2,
    SenderAddress_City,
    SenderAddress_StateOrProvince,
    SenderAddress_PostalCode,
    SenderAddress_Country,
    ReceiverAddress_Line1,
    ReceiverAddress_Line2,
    ReceiverAddress_City,
    ReceiverAddress_StateOrProvince,
    ReceiverAddress_PostalCode,
    ReceiverAddress_Country,
    TransportCompanyId
)
VALUES
(GETUTCDATE(),
    '1725 Slough Avenue', NULL, 'Scranton', 'PA', '18505', 'USA',
    '104 Main Street', NULL, 'Wilkes-Barre', 'PA', '18701', 'USA',
    1),

(GETUTCDATE(),
    '1725 Slough Avenue', NULL, 'Scranton', 'PA', '18505', 'USA',
    '880 Business Park Dr', 'Suite 12', 'Allentown', 'PA', '18109', 'USA',
    2),

(GETUTCDATE(),
    '1725 Slough Avenue', NULL, 'Scranton', 'PA', '18505', 'USA',
    '55 Market Street', NULL, 'Newark', 'NJ', '07102', 'USA',
    3);

INSERT INTO OrderProduct (OrderId, ProductsId)
VALUES
    (1, 1),
    (1, 4),
    (1, 5),
    (1, 7)
    (2, 1),
    (2, 2),
    (2, 3),
    (2, 8)
    (3, 1),
    (3, 6),
    (3, 7),
    (3, 8);