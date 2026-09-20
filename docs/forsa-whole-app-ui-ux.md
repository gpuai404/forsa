# Forsa Whole-App UI/UX Specification

**Product:** forsa  
**Platform:** Mobile-first career discovery and application companion  
**Primary idea:** Help people understand which opportunities fit them and why.

---

# 1. Product Experience Model

Forsa is not organized as a traditional job board.

The product is a continuous experience:

**Discover → Understand fit → Explore role → Apply → Track → Improve profile/resume**

The four primary areas are:

- **Discover:** find
- **Activity:** track
- **Resume AI:** create
- **Profile:** manage

**Job View** is contextual, not a navigation destination.

---

# 2. Global Visual System

## Brand
**Wordmark:** `forsa`  
**Case:** lowercase

## Core palette
- Primary blue: `#0057B8`
- Background: `#F5F7F9`
- Card: `#FFFFFF`
- Main text: `#111827`
- Muted text: `#667085`

## Typography
Inter or equivalent.

- 900: headlines
- 800: job titles
- 700/600: controls and labels
- 400: body copy

## Shapes
- Cards: 14–22 px radius
- Inputs: 12–16 px
- Pills: full radius
- Avatars: circles
- Icons: rounded, simple outline style

## Motion
**Morph → Unfold → Expand**

---

# 3. Global App Shell

## Top-level pattern
Each primary screen has:
- safe-area-aware header
- contextual content area
- bottom navigation

## Bottom navigation
1. Discover
2. Activity
3. Resume AI
4. Profile

### Persistent behavior
- Preserve scroll position between tabs.
- Avoid full-screen reloads when switching modes.
- Keep Search and Resume AI conversational states when returning.

---

# 4. 01 — Discover

## Purpose
The real home page.

It should feel like a personal career feed, not a job-board index.

## Header
- Forsa logo
- Profile avatar
- Minimal utility action if required

## Hero copy
**Discover what fits you next.**

## Search field
Telegram-inspired but unmistakably Forsa:
- rounded
- calm neutral surface
- search icon
- placeholder: `Search jobs, skills, companies...`

## Horizontal filters
- For you
- Remote
- Near me
- New today
- €50k+

Scrollable horizontally.

---

## Featured opportunity card

The most important opportunity appears first.

### Card content
- company/logo
- job title
- company
- location/work style
- employment type
- `92% Match`
- match bar
- 3 concise reasons

Example reasons:
- `3/3 core skills`
- `Preferred work style`
- `Salary aligned`

---

## Match Lens

Tap `92% Match`.

The card expands inline.

### Breakdown
- Skills — 96% overlap
- Work style — 91% overlap
- Location — 100% overlap
- Experience — 84% overlap

### Why this matters
The score becomes an explanation layer instead of marketing decoration.

---

## Opportunity stream

Below the hero card:
- standard opportunity cards
- compact metadata
- match score visible on each card
- save action

### Discover behavior
Fast scan first.
Deep understanding second.

Do not force a user to open a detail page just to understand the basic fit.

---

# 5. 02 — Search Mode

## Trigger
Tap the Discover search field.

## Transition
Discover **morphs** into Search.

The existing search field expands into the dominant element.

## Search screen content order
1. Expanded search field
2. Recent searches
3. `Matches unfolding`
4. Live result count
5. Horizontal filters
6. Unfolding result cards

## Recent searches
Examples:
- product designer
- remote
- Brussels

## Live results
As typing progresses:
- results appear underneath
- result count changes
- matching opportunities animate upward

### Signature behavior
The app should feel as if the query is physically opening the opportunity stream.

No full-page loading experience.

---

## Search states

### Empty
Show:
- recent searches
- popular categories
- lightweight discovery suggestions

### Typing
Show:
- matching result preview
- live count
- active filters

### Results
Show:
- full opportunity cards
- match score
- filters

### No results
Explain what is missing and offer nearby query/filter suggestions.

---

# 6. 03 — Job View

## Purpose
Answer:
**Why this job?**
before:
**Do you want to apply?**

## Entry
Tap a job card.

## Header
- Back to Discover
- Share
- More

## Hero area
- Job title
- Company
- Location
- employment/work style
- salary where available

## Match summary

Primary:
`92% Match`

Secondary explanation:
`Skills + work style + location + experience`

Optional compact breakdown:
- Skills
- Work style
- Location
- Experience

---

## Content sections

### The role
A readable description of the position.

### What you'll do
Bulleted responsibilities.

### Role details
Practical facts:
- Location
- Type
- Salary
- Experience

### Primary CTA
**Apply for this job**

Secondary action:
- Save

---

## Tailor Resume connection
Place a secondary action near the application flow:

**Tailor resume to this job**

This opens Resume AI with the selected job already attached as context.

---

# 7. 04 — Activity

## Purpose
Answer:
**What is happening with my applications?**

Activity is about movement, not a list of job postings.

## Main section
Application timeline.

### Example
**Application received**
Product Designer · Spotify
2 hours ago

↓

**Profile viewed**
Senior Product Designer · Stripe
5 hours ago

↓

**Next step**
You'll hear back within 3–5 days
1 day ago

---

## Saved jobs

Below the timeline:
- compact saved-job rows
- `See all` action

Saved jobs are secondary.

---

## Activity states

### Active
Use stronger icon/contrast.

### Completed
Use quieter treatment.

### Upcoming
Show clear future/next-step affordance.

### Empty
Explain that activity appears here after actions such as:
- applying
- receiving updates
- saving jobs

---

# 8. 05 — Resume AI

## Purpose
Create and improve resumes through conversation.

Resume AI should not look like a form builder.

## Header
**Forsa Resume AI**
`online · building with you`

## Conversation pattern

### User message
> 4 years recruiting, mostly tech, worked with hiring managers and candidates.

### AI response
Convert rough language into structure:
- Professional experience
- Key skills
- Summary

---

## Suggested prompts
Pinned above composer:
- Build from LinkedIn
- Improve my summary
- Tailor to this job

## Composer
- attachment
- text field
- send button

---

## Job-context flow

From Job View:
**Tailor resume to this job**

Then Resume AI opens with:
- selected job
- company
- role title
- relevant context

The user immediately understands why the AI conversation is personalized.

---

## AI interaction rules
- Keep the conversation human and direct.
- Accept rough, incomplete input.
- Show the generated structure before asking for perfection.
- Allow the user to edit AI-generated content.
- Explain what changed when tailoring to a job.

---

# 9. 06 — Profile

## Purpose
Manage the information that powers the matching system.

Profile should remain quiet.

## Header
- Avatar
- Name
- Role

## Profile strength

Example:
`Profile strength — 78%`

Supporting explanation:
`Your profile powers the match score and Resume AI.`

This establishes a direct relationship between profile completeness and product usefulness.

---

## Editable rows

### Experience
Show years/roles summary.

### Skills & preferences
Show role interests, work style, location preferences, etc.

### CV / Resume
Show upload/update state.

### Job alerts
Show current alert state.

---

## Profile behavior
- Keep rows simple.
- Use chevrons for editing.
- Avoid analytics dashboards.
- Avoid excessive settings.

---

# 10. Navigation Map

```text
Discover
 ├─ Search mode
 │   └─ Opportunity results
 ├─ Opportunity card
 │   ├─ Match Lens
 │   └─ Job View
 │        ├─ Apply
 │        └─ Tailor resume to this job
 │              └─ Resume AI (job attached)
 │
Activity
 ├─ Application timeline
 └─ Saved jobs
 │
Resume AI
 ├─ Conversation
 ├─ Resume draft
 └─ Tailor to selected job
 │
Profile
 ├─ Experience
 ├─ Skills & preferences
 ├─ CV / Resume
 └─ Job alerts
```

---

# 11. Cross-App Interaction Principles

## Principle 1 — Preserve origin
When a card opens Job View, the user should understand where they came from.

## Principle 2 — Keep context
When Job View opens Resume AI, preserve the selected job.

## Principle 3 — Explain scores
Never make 92% feel arbitrary.

## Principle 4 — Avoid duplicate navigation
Do not create a separate Jobs tab when Discover already owns job discovery.

## Principle 5 — Keep the app stream-like
The product should feel more like a messenger/productivity app than a recruitment portal.

---

# 12. Motion Storyboard

## Discover → Search
**Morph**

The search control expands without feeling like a route change.

## Search → Results
**Unfold**

Cards appear in sequence below the query.

## Match score → Match Lens
**Expand**

The score remains anchored while explanation rows reveal.

## Card → Job View
**Push forward**

The selected opportunity becomes the next context.

## Job View → Resume AI
**Continue**

The role context travels into the conversation.

---

# 13. Motion Specifications

### Micro
120–160 ms  
Use for:
- icon feedback
- chip selection
- button press

### Standard
160–220 ms  
Use for:
- focus
- filter transitions
- small control changes

### Structural
220–360 ms  
Use for:
- card expansion
- navigation
- search morph

### Stagger
80–120 ms between items  
Use for:
- unfolding result cards
- Match Lens rows

### Rules
- Prefer transform/opacity/height animations.
- Avoid long animations on core workflows.
- Do not animate every element simultaneously.
- Motion should explain a relationship between states.

---

# 14. Illustration System Across the App

Illustrations should appear when they clarify a concept or empty state.

## Discover
Minimal discovery/achievement visual only when needed.

## Search
Avoid giant illustrations; search itself is the hero interaction.

## Job View
Prefer product UI, role information, company identity, and match reasoning over decorative imagery.

## Activity
Use status icons and timeline motion before using large illustrations.

## Resume AI
Use small AI identity visual and contextual assistant iconography.

## Profile
Keep illustration use minimal.

---

# 15. Empty States

## Discover
`We’re finding opportunities that fit you.`

## Search
Recent searches + suggestions.

## Activity
`Your application activity will appear here.`

## Resume AI
Prompt the user with one simple example.

## Profile
Show profile strength and the next high-value profile improvement.

---

# 16. Loading States

Prefer content-aware skeletons:
- job title line
- company line
- metadata chips
- match bar

For search:
- show the expanded search immediately
- reveal result skeletons underneath
- transition into real result cards

Avoid full-screen spinners where possible.

---

# 17. Accessibility

- Minimum touch target: 44 × 44 px
- Support Dynamic Type
- Do not communicate state by color alone
- Maintain strong contrast for primary text
- Keep labels understandable without icons
- Support reduced-motion settings
- Ensure screen-reader labels for score breakdowns and icon-only buttons

---

# 18. Content Design

## Voice
- clear
- encouraging
- intelligent
- direct
- non-corporate
- transparent

## Avoid
- recruitment jargon
- exaggerated promises
- manipulative urgency
- empty motivational language

## Prefer
`92% Match — here’s why.`

Over:
`Amazing opportunity you cannot miss!`

---

# 19. Primary User Flows

## Flow A — Discover to Apply

```text
Open Forsa
↓
Discover what fits you next
↓
Scan top opportunity
↓
See 92% Match
↓
Tap Match Lens
↓
Understand Skills / Work style / Location / Experience
↓
Open Job View
↓
Read The role
↓
Apply for this job
↓
Activity shows Application received
```

---

## Flow B — Search to Apply

```text
Discover
↓
Tap Search
↓
Search field expands
↓
Type query
↓
Matches unfold
↓
Filter if needed
↓
Tap job card
↓
Job View
↓
Apply
```

---

## Flow C — Job to Resume AI

```text
Job View
↓
Tailor resume to this job
↓
Resume AI opens
↓
Selected job attached
↓
AI suggests tailored summary/experience/skills
↓
User edits
↓
Resume updated
```

---

## Flow D — Profile improvement

```text
Profile
↓
Profile strength 78%
↓
See why
↓
Update missing Experience / Skills / CV
↓
Profile strength improves
↓
New information powers matching + Resume AI
```

---

# 20. Screen-by-Screen Component Inventory

## Discover
- App header
- Avatar
- Search field
- Filter chips
- Featured opportunity card
- Match score
- Match bar
- Match Lens
- Opportunity cards
- Save icon
- Bottom nav

## Search
- Back button
- Expanded search
- Close action
- Recent search chips
- Match unfolding label
- Result count
- Filter chips
- Opportunity cards
- Bottom nav

## Job View
- Back
- Share
- Job header
- Company logo
- Match score
- Match explanation
- Role section
- Responsibilities
- Role details
- Save
- Tailor resume
- Apply
- Optional bottom action area

## Activity
- Header
- Timeline
- Status icons
- Event rows
- Saved jobs
- See all
- Bottom nav

## Resume AI
- Back
- AI identity
- Online status
- Message bubbles
- AI content card
- Suggested prompt chips
- Attachment
- Composer
- Send

## Profile
- Avatar
- Name/role
- Profile strength
- Strength explanation
- Settings rows
- Chevrons
- Bottom nav

---

# 21. Component Relationship Rules

A single component should communicate one concept.

Examples:
- Match score = fit
- Match Lens = reason
- Match bar = magnitude
- Filter chip = constraint
- Timeline item = movement
- Suggested prompt = next action
- Profile strength = data completeness

Avoid combining too many meanings into a single visual object.

---

# 22. Product-Level Design Guardrails

### Forsa should feel like
- a personal career feed
- a smart messenger
- a transparent productivity tool
- a calm decision-support product

### Forsa should not feel like
- a generic job board
- a recruitment CRM
- an HR admin dashboard
- a flashy AI demo
- a speculative 3D “career radar”

---

# 23. Signature Forsa Experience

The product identity comes from three connected interactions:

**1. Match Lens**  
Understand why a role fits.

**2. Unfolding Search**  
Search becomes a live stream of opportunities.

**3. Conversational Resume AI**  
Turn rough career input into useful, job-specific material.

Together they create one coherent interaction language:

**Find → Explain → Create → Track**
